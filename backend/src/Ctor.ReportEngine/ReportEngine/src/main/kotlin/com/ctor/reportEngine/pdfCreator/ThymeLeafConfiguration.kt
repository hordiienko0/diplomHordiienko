package com.ctor.reportEngine.pdfCreator

import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.thymeleaf.templatemode.TemplateMode
import org.thymeleaf.templateresolver.ClassLoaderTemplateResolver


@Configuration
class ThymeLeafConfiguration {
    @Bean
    fun templateResolver() : ClassLoaderTemplateResolver? {
        val pdfTemplateResolver = ClassLoaderTemplateResolver()
        pdfTemplateResolver.prefix = "templates/"
        pdfTemplateResolver.suffix = ".html"
        pdfTemplateResolver.templateMode = TemplateMode.HTML
        pdfTemplateResolver.characterEncoding = "UTF-8"
        pdfTemplateResolver.order = 1
        return pdfTemplateResolver
    }
}