package com.ctor.reportEngine

import com.ctor.reportEngine.bus.IBus
import org.hibernate.boot.model.naming.ImplicitNamingStrategy
import org.hibernate.boot.model.naming.ImplicitNamingStrategyLegacyJpaImpl
import org.hibernate.boot.model.naming.PhysicalNamingStrategy
import org.hibernate.boot.model.naming.PhysicalNamingStrategyStandardImpl
import org.springframework.beans.factory.annotation.Autowired
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration


@Configuration
class AppConfiguration {
    @Autowired lateinit var bus: IBus

    @Bean
    fun physical(): PhysicalNamingStrategy {
        return PhysicalNamingStrategyStandardImpl()
    }

    @Bean
    fun implicit(): ImplicitNamingStrategy {
        return ImplicitNamingStrategyLegacyJpaImpl()
    }

}