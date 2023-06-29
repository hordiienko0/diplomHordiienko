package com.ctor.reportEngine

import org.apache.commons.io.IOUtils
import org.springframework.core.io.Resource
import org.springframework.core.io.UrlResource
import org.springframework.stereotype.Service
import java.io.IOException
import java.io.InputStream
import java.nio.file.Path
import java.util.*


@Service
class ImageConverterService : IImageConverterService {
    override fun convertToBase64(path: Path): String {
        var imageAsBytes = ByteArray(0)
        try {
            val resource: Resource = UrlResource(path.toUri())
            val inputStream: InputStream = resource.inputStream
            imageAsBytes = IOUtils.toByteArray(inputStream)
        } catch (e: IOException) {
            println("\n File read Exception")
        }
        return Base64.getEncoder().encodeToString(imageAsBytes)
    }
}