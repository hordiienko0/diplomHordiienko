package com.ctor.reportEngine.bus.events

import com.ctor.reportEngine.db.entities.Project

class ReportCreatedEvent(var ProjectId: Long, var UserId: Long): Event(ReportCreatedEvent::class.java.simpleName) {
}