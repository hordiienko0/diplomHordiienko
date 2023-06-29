package com.ctor.reportEngine.db.entities

import javax.persistence.Entity
import javax.persistence.Id
import javax.persistence.JoinColumn
import javax.persistence.OneToOne

@Entity(name = "Company")
class Company(@Id var Id: Long, var CompanyName: String, var CompanyLogoId: Long?) {
    @OneToOne
    @JoinColumn(name = "CompanyLogoId", insertable = false, updatable = false)
    var CompanyLogo: CompanyLogo? = null
}