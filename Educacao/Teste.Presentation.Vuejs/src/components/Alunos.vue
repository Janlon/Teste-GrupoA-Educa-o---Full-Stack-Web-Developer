<template>
    <main>

        <v-card>
            <v-card-title>
                <v-text-field v-model="search" append-icon="mdi-magnify" label="Pesquisar" single-line hide-details ></v-text-field>
            </v-card-title>
        </v-card>

        <v-data-table :headers="headers" :items="alunos" :search="search" sort-by="ra" class="elevation-1">
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
                                            <v-text-field v-model="editedItem.nome" :error-messages="nomeErrors" required label="Nome"></v-text-field>
                                        </v-col>
                                        <v-col cols="12" sm="6" md="4">
                                            <v-text-field v-model="editedItem.email" :error-messages="emailErrors" required label="Email" ></v-text-field>
                                        </v-col>
                                        <v-col cols="12" sm="6" md="4">
                                            <v-text-field v-model="editedItem.cpf" :error-messages="cpfErrors" required label="CPF"></v-text-field>
                                        </v-col>
                                        <v-col cols="12" sm="6" md="4">
                                            <v-text-field v-model="editedItem.ra" :error-messages="raErrors" required label="Registro Acadêmico"></v-text-field>
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
                this.$v.$reset()
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

                this.$v.$touch()
                let errors = []

                if(this.editedItem.nome == "")
                {
                    errors.push('Nome é obrigatório.')
                    return errors
                }

                if(this.editedItem.email == "")
                {
                    errors.push('E-mail é obrigatório')
                    return errors
                }

                if(this.editedItem.cpf == "")
                {
                    errors.push('CPF é obrigatório')
                    return errors
                }

                if(this.editedItem.ra == "")
                {
                    errors.push('RA é obrigatório')
                    return errors
                }

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
                this.$v.$reset()
                this.close();
            }     
        },
        validations: {
            nome: { required, maxLength: maxLength(300) },
            email: { required, email , maxLength: maxLength(256) },
            cpf: { required, maxLength: maxLength(14) },
            ra: { required, maxLength: maxLength(14) }
        },
        computed: {
            formTitle () {
                return this.editedIndex === -1 ? 'Cadastrar aluno' : 'Editar registro'
            },
             nomeErrors () {
                const errors = []
                if (!this.$v.nome.$dirty) return errors
                !this.$v.nome.maxLength && errors.push('Nome possui mais de 300 caracteres')
                !this.$v.nome.required && errors.push('Nome é obrigatório.')
                return errors
            },
            emailErrors () {
                const errors = []
                if (!this.$v.email.$dirty) return errors
                !this.$v.email.email && errors.push('Email inválido')
                !this.$v.email.maxLength && errors.push('Email possui mais de 256 caracteres')
                !this.$v.email.required && errors.push('E-mail é obrigatório')
                return errors
            },
            cpfErrors () {
                const errors = []
                if (!this.$v.cpf.$dirty) return errors
                !this.$v.cpf.maxLength && errors.push('CPF possui mais de 14 caracteres')
                !this.$v.cpf.required && errors.push('CPF é obrigatório')
                return errors
            },
            raErrors () {
                const errors = []
                if (!this.$v.ra.$dirty) return errors
                !this.$v.ra.maxLength && errors.push('RA possui mais de 14 caracteres')
                !this.$v.ra.required && errors.push('RA é obrigatório')
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
                { text: 'RA', value: 'ra', sortable: true, editable: false },
                { text: 'Nome', value: 'nome', sortable: true },
                { text: 'Email', value: 'email', sortable: true },
                { text: 'CPF', value: 'cpf', sortable: true },
                { text: 'Ações', value: 'actions', sortable: false }
            ],
            alunos: [],
            editedIndex: -1,
            editedItem: {ra:'', nome: '', email: '', cpf: ''},
            defaultItem: { id:0, ra:'', nome: '', email: '', cpf: ''}
        }),
        mounted() { api.get('Aluno').then(response => {
                        this.alunos = response.data;
                        })
                    }
    }
</script>
