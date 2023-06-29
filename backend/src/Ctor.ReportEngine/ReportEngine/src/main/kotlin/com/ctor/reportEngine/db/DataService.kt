package com.ctor.reportEngine.db

import com.ctor.reportEngine.db.entities.Company
import com.ctor.reportEngine.db.entities.Document
import com.ctor.reportEngine.db.entities.Material
import com.ctor.reportEngine.db.entities.ProjectDocument
import com.ctor.reportEngine.db.repos.ICompanyRepository
import com.ctor.reportEngine.db.repos.IMaterialRepository
import com.ctor.reportEngine.db.repos.ProjectDocumentInsertRepository
import org.aspectj.weaver.tools.cache.SimpleCacheFactory.path
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.stereotype.Service
import java.util.*

@Service
class DataService :IDataService {
    @Autowired
    private lateinit var materialRepository: IMaterialRepository
    @Autowired
    private lateinit var companyRepository: ICompanyRepository
    @Autowired
    private lateinit var projectDocumentInsertRepository: ProjectDocumentInsertRepository
    override fun getMaterials(projectId: Long): List<Material> {
        return materialRepository.findByProjectId(projectId)
    }

    override fun getCompany(projectId: Long): Company? {
        return companyRepository.finByProjectId(projectId)
    }

    override fun addReport(pdfName: String, Path: String, Link: String, ProjectId: Long) {
        val document = Document(pdfName, Path, Link)
        projectDocumentInsertRepository.insertDocumentWithEntityManager(document)
        val projectDocument =  ProjectDocument(
            ProjectId,
            Date(),
            Document = document
        )
        projectDocumentInsertRepository.insertProjectDocumentWithEntityManager(projectDocument)
    }

}