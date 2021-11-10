<template>
    <main>

        <v-card>
            <v-card-title>
                <v-text-field v-model="search" append-icon="mdi-magnify" label="Pesquisar" single-line hide-details ></v-text-field>
            </v-card-title>
        </v-card>

        <v-data-table :headers="headers" :items="alunos" :search="search" sort-by="nome" class="elevation-1">
            <template v-slot:top>

                <v-toolbar flat>

                    <v-toolbar-title>Alunos</v-toolbar-title>
                    <v-divider class="mx-4" inset vertical></v-divider>
                    <v-spacer></v-spacer>

                    <v-dialog v-model="dialog" max-width="500px">
                        <template v-slot:activator="{ on, attrs }">
                            <v-btn color="primary" dark class="mb-2"  v-bind="attrs" v-on="on">Cadastrar Aluno</v-btn>
                        </template>
                        <v-card>
                            <v-card-title>
                                <span class="text-h5">{{ formTitle }}</span>
                            </v-card-title>
                            <v-card-text>
                                <v-container>
                                    <v-hover>
                                        <v-row>
                                        <v-col cols="12" sm="6" md="4">
                                            <v-text-field v-model="editedItem.nome" :error-messages="nameErrors" required label="Nome"></v-text-field>
                                        </v-col>
                                        <v-col cols="12" sm="6" md="4">
                                            <v-text-field v-model="editedItem.email" :error-messages="emailErrors" required label="Email" ></v-text-field>
                                        </v-col>
                                        <v-col cols="12" sm="6" md="4">
                                            <v-text-field v-model="editedItem.cpf" required label="CPF"></v-text-field>
                                        </v-col>
                                        <v-col cols="12" sm="6" md="4">
                                            <v-card-actions>
                                                <v-spacer></v-spacer>
                                                <v-btn color="blue darken-1" text @click="close"> Cancelar </v-btn>
                                                <v-btn color="blue darken-1" text @click="save"> Salvar </v-btn>
                                            </v-card-actions>
                                        </v-col>
                                        </v-row>
                                    </v-hover>
                                </v-container>
                            </v-card-text>
                        </v-card>
                    </v-dialog>

                    <v-dialog v-model="dialogDelete" max-width="500px">
                        <v-card>
                            <v-card-title class="text-h5">Esta ação irá excluir o registro.</v-card-title>
                            <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="blue darken-1" text @click="closeDelete">Cancelar</v-btn>
                            <v-btn color="blue darken-1" text @click="deleteItemConfirm">Excluir</v-btn>
                            <v-spacer></v-spacer>
                            </v-card-actions>
                        </v-card>
                    </v-dialog>

                </v-toolbar>
            </template>
            <template v-slot:item.actions={item}>
                <v-icon small class="mr-2" @click="editItem(item)" > mdi-pencil </v-icon>
                <v-icon small @click="deleteItem(item)" > mdi-delete </v-icon> 
            </template>
            <template v-slot:no-data>
                <v-btn color="primary"  @click="initialize" > Reset </v-btn>
            </template>   
        </v-data-table>
    </main>
</template>

<script>
    import api from '@/services/api.js';
    import { required, maxLength, email } from 'vuelidate/lib/validators'

    export default {
        name: 'Alunos',
        methods: {
            initialize () {
                this.alunos = []
            },
            editItem (item) {
                this.editedIndex = this.alunos.indexOf(item)
                this.editedItem = Object.assign({}, item)
                this.dialog = true
            },
            deleteItem (item) {
                this.editedIndex = this.alunos.indexOf(item)
                this.editedItem = Object.assign({}, item)
                this.dialogDelete = true
            },
            deleteItemConfirm () {
                this.closeDelete();
                if(this.editedItem.id > 0){
                    api.delete(`Aluno/${this.editedItem.id}`).then(() => {
                        api.get('Aluno').then(response => {
                            this.alunos = response.data;
                        });
                    }); 
                }
            },
            close () {
                this.dialog = false
                this.$nextTick(() => {
                this.editedItem = Object.assign({}, this.defaultItem)
                this.editedIndex = -1
                })
            },
            closeDelete () {
                this.dialogDelete = false
                this.$nextTick(() => {
                this.editedItem = Object.assign({}, this.defaultItem)
                this.editedIndex = -1
                })
            },
            save () {
                if (this.editedIndex > -1) 
                {
                    api.put("Aluno", this.editedItem).then(() => {
                        api.get('Aluno').then(response => {
                            this.alunos = response.data;
                        });
                    });                                                                                                                                                                                                                                                                                                                                             
                } 
                else 
                {
                    api.post("Aluno", this.editedItem).then(() => {
                        api.get('Aluno').then(response => {
                            this.alunos = response.data;
                        });
                    }); 
                }
                this.close();
            },       
        },
        validations: {
            name: { required, maxLength: maxLength(300) },
            email: { required, email , maxLength: maxLength(256) }
        },
        computed: {
            formTitle () {
                return this.editedIndex === -1 ? 'Cadastrar aluno' : 'Editar registro'
            },
             nameErrors () {
                const errors = []
                if (!this.$v.name.$dirty) return errors
                !this.$v.name.maxLength && errors.push('Nome possui mais de 300 caracteres')
                !this.$v.name.required && errors.push('Nome é obrigatório.')
                return errors
            },
            emailErrors () {
                const errors = []
                if (!this.$v.email.$dirty) return errors
                !this.$v.email.email && errors.push('Email inválido')
                !this.$v.email.maxLength && errors.push('Email possui mais de 256 caracteres')
                !this.$v.email.required && errors.push('E-mail é obrigatório')
                return errors
            }
        },
        watch: {
            dialog (val) {
                val || this.close()
            },
            dialogDelete (val) {
                val || this.closeDelete()
            },
        },
        created () {
            this.initialize()
        },
        data: () => ({
            search: '',
            dialog: false,
            dialogDelete: false,
            headers: [
                { text: 'Registo Acadêmico', align: 'start', sortable: true, value: 'id'},
                { text: 'Nome', value: 'nome' },
                { text: 'Email', value: 'email' },
                { text: 'CPF', value: 'cpf' },
                { text: 'Ações', value: 'actions', sortable: false },
            ],
            alunos: [],
            editedIndex: -1,
            editedItem: {nome: '', email: '', cpf: ''},
            defaultItem: {id:0, nome: '', email: '', cpf: ''},
        }),
        mounted() { api.get('Aluno').then(response => {
                        this.alunos = response.data;
                    })
            }
    }
</script>
