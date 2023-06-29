package com.ctor.reportEngine.db

import com.ctor.reportEngine.db.entities.Company
import com.ctor.reportEngine.db.entities.Material
import com.ctor.reportEngine.db.entities.Project

interface IDataService {
    fun getMaterials(projectId: Long):List<Material>
    fun getCompany(projectId: Long):Company?
    fun addReport(pdfName: String, Path: String, Link: String, ProjectId: Long)
}