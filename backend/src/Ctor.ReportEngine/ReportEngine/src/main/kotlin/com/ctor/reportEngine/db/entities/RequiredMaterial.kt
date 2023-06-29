package com.ctor.reportEngine.db.entities

import javax.persistence.Entity
import javax.persistence.Id
import javax.persistence.JoinColumn
import javax.persistence.ManyToOne
import javax.persistence.OneToOne

@Entity(name = "RequiredMaterials")
class RequiredMaterial(@Id var Id: Long, var BuildingId: Long, var MaterialId: Long, var Amount: Long) {
    @OneToOne
    @JoinColumn(name = "MaterialId", insertable = false, updatable = false)
    lateinit var Material: Material

    @ManyToOne
    @JoinColumn(name = "BuildingId", insertable = false, updatable = false)
    lateinit var Building: Building

}