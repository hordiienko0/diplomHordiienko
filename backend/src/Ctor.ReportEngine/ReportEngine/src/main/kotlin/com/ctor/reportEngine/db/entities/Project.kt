package com.ctor.reportEngine.db.entities

import javax.persistence.*

@Entity(name="Project")
class Project(@Id var Id: Long, var CompanyId: Long) {
    @ManyToOne
    @JoinColumn(name = "CompanyId", insertable = false, updatable = false)
    lateinit var Company: Company
}