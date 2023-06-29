package com.ctor.reportEngine.db.entities

import java.util.Date
import javax.persistence.Entity
import javax.persistence.Id
import javax.persistence.JoinColumn
import javax.persistence.ManyToOne

@Entity(name = "Material")
class Material(
    @Id var Id: Long,
    var MaterialTypeId: Long,
    var MeasurementId: Long,
    var Amount: Int,
    var CompanyId: Long,
    var Price: Double,
    var Date: Date,
    var CompanyAddress: String,
    var CompanyName: String
) {
    @ManyToOne
    @JoinColumn(name = "MaterialTypeId", insertable = false, updatable = false)
    lateinit var MaterialType: MaterialType

    @ManyToOne
    @JoinColumn(name = "MeasurementId", insertable = false, updatable = false)
    lateinit var Measurement: Measurement

}