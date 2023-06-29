package com.ctor.reportEngine.db.repos

import com.ctor.reportEngine.db.entities.Company
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.data.jpa.repository.Query
import org.springframework.data.repository.query.Param

interface ICompanyRepository : JpaRepository<Company, Long> {
    @Query("SELECT e.Company FROM Project e WHERE e.Id = :ProjectId")
    fun finByProjectId(@Param("ProjectId") ProjectId: Long):Company?
}