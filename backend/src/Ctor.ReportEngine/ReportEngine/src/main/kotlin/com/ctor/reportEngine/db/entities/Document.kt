package com.ctor.reportEngine.db.entities

import javax.persistence.*

@Entity(name = "Document")
class Document(
    var Name : String,
    var Path : String,
    var Link: String,
    @Id @GeneratedValue(strategy = GenerationType.IDENTITY) var Id : Long? = null,
) {
    @OneToOne
    @JoinColumn(name = "ProjectDocumentId")
    var ProjectDocument : ProjectDocument? = null
}