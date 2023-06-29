package com.ctor.reportEngine.db.entities

import javax.persistence.Entity
import javax.persistence.Id

@Entity(name = "CompanyLogo")
class CompanyLogo(@Id var Id: Long, var CompanyId: Long, var Path: String) {
}