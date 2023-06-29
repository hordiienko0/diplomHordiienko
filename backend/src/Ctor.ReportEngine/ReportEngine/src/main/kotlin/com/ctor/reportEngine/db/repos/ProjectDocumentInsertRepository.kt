package com.ctor.reportEngine.db.repos

import com.ctor.reportEngine.db.entities.Document
import com.ctor.reportEngine.db.entities.ProjectDocument
import org.springframework.stereotype.Repository
import javax.persistence.EntityManager
import javax.persistence.PersistenceContext
import javax.transaction.Transactional


@Repository
class ProjectDocumentInsertRepository{
    @PersistenceContext
    private val entityManager: EntityManager? = null

    @Transactional
    fun insertProjectDocumentWithEntityManager(ProjectDocument: ProjectDocument) {
        entityManager!!.persist(ProjectDocument)
    }

    @Transactional
    fun insertDocumentWithEntityManager(Document: Document) {
        return entityManager!!.persist(Document)

    }

}