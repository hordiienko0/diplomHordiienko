package com.ctor.reportEngine.pdfCreator

import java.nio.file.Path

interface IPdfCreatorService {
    fun generatePdfFile(templateName: String, data: Map<String, Any>, pdfFileName: String): String
}