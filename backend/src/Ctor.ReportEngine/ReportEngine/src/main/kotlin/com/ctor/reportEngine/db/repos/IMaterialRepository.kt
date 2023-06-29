package com.ctor.reportEngine.db.repos

import com.ctor.reportEngine.db.entities.Material
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.data.jpa.repository.Query
import org.springframework.data.repository.query.Param


interface IMaterialRepository : JpaRepository<Material, Long> {
    @Query("SELECT e.Material FROM RequiredMaterials e WHERE e.Building.Project.Id = :ProjectId")
    fun findByProjectId(@Param("ProjectId")ProjectId: Long): List<Material>
}