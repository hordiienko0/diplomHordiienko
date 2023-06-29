package com.ctor.reportEngine.db.entities

import javax.persistence.Entity
import javax.persistence.Id
import javax.persistence.JoinColumn
import javax.persistence.ManyToOne
import javax.persistence.OneToMany

@Entity(name = "Building")
class Building(@Id var Id: Long, ProjectId: Long) {
    @ManyToOne
    @JoinColumn(name = "ProjectId", insertable = false, updatable = false)
    lateinit var Project: Project

}