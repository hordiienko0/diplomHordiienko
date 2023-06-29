package com.ctor.reportEngine.pdfCreator

import org.slf4j.Logger
import org.slf4j.LoggerFactory
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.beans.factory.annotation.Value
import org.springframework.stereotype.Service
import org.thymeleaf.TemplateEngine
import org.thymeleaf.context.Context
import org.xhtmlrenderer.pdf.ITextRenderer
import java.io.FileOutputStream
import java.nio.file.Files
import java.nio.file.Path
import java.nio.file.Paths


@Service
class PdfCreatorService : IPdfCreatorService {
    private val logger: Logger = LoggerFactory.getLogger(PdfCreatorService::class.java)

    @Autowired
    private lateinit var templateEngine: TemplateEngine

    @Value("\${directory}")
    private lateinit var directory: String

    override fun generatePdfFile(templateName: String, data: Map<String, Any>, pdfFileName: String): String {
        val context = Context()
        context.setVariables(data)
        val htmlContent = templateEngine.process(templateName, context)
        val path = Paths.get(directory, "Reports")
        if (!Files.exists(path))
            Files.createDirectory(Paths.get(path.toUri()))
        val fileOutputStream = FileOutputStream(Paths.get(path.toString(), pdfFileName).toFile())
        val renderer = ITextRenderer()
        renderer.setDocumentFromString(htmlContent)
        renderer.layout()
        renderer.createPDF(fileOutputStream, false)
        renderer.finishPDF()
        return Paths.get("Reports", pdfFileName).toString()
    }
}