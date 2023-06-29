package com.ctor.reportEngine

import com.ctor.reportEngine.bus.IBus
import com.ctor.reportEngine.bus.IReceiver
import com.ctor.reportEngine.bus.Receiver
import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication

@SpringBootApplication
class Engine(bus: IBus, receiver: Receiver) {
    companion object{
        @JvmStatic
        fun main(args: Array<String>) {
            runApplication<Engine>(*args)
        }
    }

    init {
        startListening(bus, receiver)
    }
    private final fun startListening(bus: IBus, receiver: IReceiver){
        bus.subscribe(receiver)
    }


}