package com.ctor.reportEngine

import java.nio.file.Path

interface IImageConverterService {
    fun convertToBase64(path: Path): String
}