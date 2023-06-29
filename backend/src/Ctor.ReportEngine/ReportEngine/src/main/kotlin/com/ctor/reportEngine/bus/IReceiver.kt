package com.ctor.reportEngine.bus

interface IReceiver {
    fun receiveMessage(bytes: ByteArray)
}