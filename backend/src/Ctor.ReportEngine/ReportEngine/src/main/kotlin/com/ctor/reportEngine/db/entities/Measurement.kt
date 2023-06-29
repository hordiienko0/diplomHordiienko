package com.ctor.reportEngine.db.entities

import javax.persistence.Entity
import javax.persistence.Id

@Entity(name = "Measurement")
class Measurement(@Id var Id: Long, var Name: String) {
}