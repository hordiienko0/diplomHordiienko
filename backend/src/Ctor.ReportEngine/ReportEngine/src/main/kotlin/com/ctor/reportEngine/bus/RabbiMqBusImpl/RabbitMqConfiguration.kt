package com.ctor.reportEngine.bus.RabbiMqBusImpl

import com.ctor.reportEngine.bus.IBus
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.context.annotation.Profile

@Configuration
@Profile("development")
class RabbitMqConfiguration {

    @Bean
    fun bus(): IBus {
        return RabbitMqBus("localhost", 5672, "guest", "guest")
    }
}