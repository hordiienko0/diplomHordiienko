package com.ctor.reportEngine.bus.RabbiMqBusImpl

import com.ctor.reportEngine.bus.IBus
import com.ctor.reportEngine.bus.IReceiver
import com.ctor.reportEngine.bus.events.CreateReportEvent
import com.ctor.reportEngine.bus.events.ReportCreatedEvent
import com.google.gson.Gson
import org.slf4j.Logger
import org.slf4j.LoggerFactory
import org.springframework.amqp.core.AmqpAdmin
import org.springframework.amqp.core.Queue
import org.springframework.amqp.core.TopicExchange
import org.springframework.amqp.rabbit.connection.CachingConnectionFactory
import org.springframework.amqp.rabbit.connection.ConnectionFactory
import org.springframework.amqp.rabbit.core.RabbitAdmin
import org.springframework.amqp.rabbit.core.RabbitTemplate
import org.springframework.amqp.rabbit.listener.SimpleMessageListenerContainer
import org.springframework.amqp.rabbit.listener.adapter.MessageListenerAdapter
import org.springframework.beans.factory.annotation.Value
import org.springframework.context.annotation.Bean
import org.springframework.stereotype.Service
import javax.annotation.PostConstruct


class RabbitMqBus(
        private var host: String,
        private var port: Int,
        private var userName: String,
        private var password: String
) : IBus {
    private var gson = Gson()
    private var logger: Logger = LoggerFactory.getLogger(RabbitMqBus::class.java)
    private var connectionFactory = connectionFactory()

    companion object {
        var topicExchangeName = ""
        var subscribeQueueName: String = CreateReportEvent::class.java.simpleName
        var pushQueueName: String = ReportCreatedEvent::class.java.simpleName
    }

    @PostConstruct
    fun declareQueues() {
        val ampqAdmin = amqpAdmin()
        ampqAdmin.declareQueue(Queue(subscribeQueueName, false))
        ampqAdmin.declareQueue(Queue(pushQueueName, false))
    }

    override fun push(report: ReportCreatedEvent) {
        val rabbitTemplate = RabbitTemplate(connectionFactory)
        logger.info("Event pushed ${report.javaClass.simpleName}")
        rabbitTemplate.convertAndSend(
            topicExchangeName,
            pushQueueName,
            gson.toJson(report)
        )
    }

    override fun subscribe(receiver: IReceiver) {
        logger.info("Start listening to $subscribeQueueName")
        startListening(receiver)
    }

    private fun startListening(receiver: IReceiver) {
        val listenerAdapter = listenerAdapter(receiver)
        val container = container(listenerAdapter)
        container.start()
    }

    private fun amqpAdmin(): AmqpAdmin {
        return RabbitAdmin(connectionFactory)
    }

    @Bean
    fun queue(): Queue {
        return Queue(subscribeQueueName, false)
    }

    @Bean
    fun exchange(): TopicExchange {
        return TopicExchange(topicExchangeName)
    }

    private fun container(
            listenerAdapter: MessageListenerAdapter
    ): SimpleMessageListenerContainer {
        val container = SimpleMessageListenerContainer()
        container.connectionFactory = connectionFactory
        container.setQueueNames(subscribeQueueName)
        container.setMessageListener(listenerAdapter)
        return container
    }

    private fun connectionFactory(): ConnectionFactory {
        val connectionFactory = CachingConnectionFactory()
        connectionFactory.setAddresses(host)
        connectionFactory.port = port
        connectionFactory.username = userName
        connectionFactory.setPassword(password)
        return connectionFactory
    }

    private fun listenerAdapter(receiver: IReceiver): MessageListenerAdapter {
        return MessageListenerAdapter(receiver, receiver::receiveMessage.name)
    }
}