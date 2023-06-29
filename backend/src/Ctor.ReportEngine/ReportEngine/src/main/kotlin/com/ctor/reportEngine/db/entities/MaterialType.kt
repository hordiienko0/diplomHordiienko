package com.ctor.reportEngine.db.entities

import javax.persistence.Entity
import javax.persistence.Id

@Entity(name = "MaterialType")
class MaterialType(@Id var Id: Long, var Name: String) {
}