package com.ctor.reportEngine.bus.events

class CreateReportEvent(var ProjectId: Long, var UserId: Long): Event(CreateReportEvent::class.java.simpleName) {
}