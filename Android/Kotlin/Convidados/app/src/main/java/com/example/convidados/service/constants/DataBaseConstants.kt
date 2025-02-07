package com.example.convidados.service.constants

class DataBaseConstants {
    // tabelas disponiveis no banco de dados com suas colunas

    object GUEST{
        const val TABLE_NAME = "Guest"

        object COLUMNS {
            const val ID = "id"
            const val NAME = "name"
            const val PRESENCE = "presence"
        }
    }

}