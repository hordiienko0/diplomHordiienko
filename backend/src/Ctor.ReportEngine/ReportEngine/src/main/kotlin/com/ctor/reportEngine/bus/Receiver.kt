package com.ctor.reportEngine.bus

import com.ctor.reportEngine.IImageConverterService
import com.ctor.reportEngine.bus.events.CreateReportEvent
import com.ctor.reportEngine.bus.events.ReportCreatedEvent
import com.ctor.reportEngine.db.IDataService
import com.ctor.reportEngine.db.entities.Document
import com.ctor.reportEngine.db.entities.ProjectDocument
import com.ctor.reportEngine.db.repos.ProjectDocumentInsertRepository
import com.ctor.reportEngine.pdfCreator.IPdfCreatorService
import com.google.gson.Gson
import org.aspectj.weaver.tools.cache.SimpleCacheFactory
import org.slf4j.Logger
import org.slf4j.LoggerFactory
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.beans.factory.annotation.Value
import org.springframework.stereotype.Service
import java.nio.file.Paths
import java.util.*
import kotlin.collections.HashMap

@Service
class Receiver : IReceiver {
    private var gson = Gson()
    private var logger: Logger = LoggerFactory.getLogger(Receiver::class.java)

    @Autowired
    private lateinit var bus: IBus

    @Autowired
    private lateinit var dataService: IDataService

    @Autowired
    private lateinit var pdfCreator: IPdfCreatorService

    @Autowired
    private lateinit var imageConverterService: IImageConverterService



    @Value("\${directory}")
    private lateinit var directory: String
    override fun receiveMessage(bytes: ByteArray) {
        try {
            val message = gson.fromJson(String(bytes), CreateReportEvent::class.java)
            val company = dataService.getCompany(message.ProjectId)
            if (company != null) {
                var image = ""
                if (company.CompanyLogo != null) {
                    val imagePath = Paths.get(directory, company.CompanyLogo?.Path)
                    image = "data:image/png;base64,${imageConverterService.convertToBase64(imagePath)}"
                }
                val materials = dataService.getMaterials(message.ProjectId)
                val data: MutableMap<String, Any> = HashMap()
                data["totalSum"] = materials.sumOf { it.Price }
                data["materials"] = materials
                data["company"] = company
                data["companyImage"] = image
                val pdfName = "report_${UUID.randomUUID()}.pdf"
                val path = pdfCreator.generatePdfFile("report", data, pdfName)
                dataService.addReport(pdfName, path
                    .replace("\\", "\\\\"), path.replace("\\", "/"), message.ProjectId)
                bus.push(ReportCreatedEvent(message.ProjectId, message.UserId))
            } else {
                throw IllegalArgumentException("No company for this projectId exists")
            }

        } catch (exception: Exception) {
            logger.error("Fail to process event with exception - ${exception.message}")
            logger.trace(exception.stackTrace.contentToString())
        }
    }
}