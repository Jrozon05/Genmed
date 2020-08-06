<template>
    <div>
        <CAlert color="dark">
            <strong>MOTIVO DE CONSULTA</strong>
        </CAlert>
        <editor
        v-model="consulta"
        placeholder=""
        theme="snow"
        @change="checkConsulta($event)"
        :class="{ 'invalid' : !isConsultaValid && isConsultaValid != null }"
        >
        </editor>
        <p v-if="!isConsultaValid && isConsultaValid != null" :class="{'invalid-label' : !isConsultaValid}">El motivo de consulta es requerido</p>
    </div>
</template>

<script>
import { required } from 'vuelidate/lib/validators'

export default {
        data () {
            return {
                consulta: '',
                isConsultaValid: null
            }
        },
        validations: {
            paciente: {
                consulta: {
                    required
                }
            }
        },
        methods: {
            checkConsulta (event) {
                if (event === '') {
                    this.isConsultaValid = false
                } else {
                    this.isConsultaValid = true
                }
            },
            validate () {
                 var isValid = this.isConsultaValid
                 if (isValid) {
                    this.$store.dispatch('paciente/setMotivoConsulta', this.$data)
                 }
                 return isValid
            }
        }
    }
</script>

<style>
    .invalid {
        border: 1px solid #e55353;
    }

    .invalid-label {
        width: 100%;
        margin-top: 0.25rem;
        font-size: 80%;
        color: #e55353
    }
    .ql-editor, .ql-blank {
        min-height: 350px;
    }
</style>
