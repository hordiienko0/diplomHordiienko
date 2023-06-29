package com.ctor.reportEngine.bus

import com.ctor.reportEngine.bus.events.ReportCreatedEvent

interface IBus {
    fun push(report: ReportCreatedEvent)
    fun subscribe(receiver: IReceiver)
}