package com.ctor.reportEngine.db.entities

import java.util.Date
import javax.persistence.*

@Entity(name = "ProjectDocument")
class ProjectDocument(
        var ProjectId: Long,
        var Created: Date,
        var BuildingId: Long? = null,
        @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
        var Id: Long? = null,
        @OneToOne
        @JoinColumn(name = "DocumentId")
        var Document : Document? = null
) {

}