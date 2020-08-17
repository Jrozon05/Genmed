<template>
    <div>
        <CRow>
            <CCol sm="4">
                <CCard>
                <CCardHeader>
                    <strong>Formulario Doctor</strong>
                </CCardHeader>
                <CCardBody>
                    <CRow>
                        <CCol sm="12">
                            <CAlert color="danger" closeButton :show.sync="alert" class="alert-dismissible" v-if="message">
                                {{ message }}
                            </CAlert>
                        </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Nombre(s)"
                        placeholder="Introduzca su nombre(s)"
                        v-model="doctor.nombre"
                        @input="$v.doctor.nombre.$touch()"
                        invalid-feedback="El nombre es un campo requerido"
                        :is-valid="!$v.doctor.nombre.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Apellido(s)"
                        placeholder="Introduzca su apellido(s)"
                        v-model="doctor.apellido"
                        @input="$v.doctor.apellido.$touch()"
                        invalid-feedback="El apellido es un campo requerido"
                        :is-valid="!$v.doctor.apellido.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Posición"
                        placeholder="Introduzca su posición"
                        v-model="doctor.posicion"
                        @input="$v.doctor.posicion.$touch()"
                        invalid-feedback="La posicion es un campo requerido"
                        :is-valid="!$v.doctor.posicion.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                    <div class="form-group">
                        <label>Usuario</label>
                        <v-select label="nombreUsuario"
                            :options="usuarios"
                            :value="usuarios.usuarioId"
                            v-model="doctor.usuarioId"
                            placeholder="Seleccionar el usuario"
                            @input="checkUsuarioValue"
                            :class="{ 'invalid' : !isUsuarioValid && isUsuarioValid != null }">
                        </v-select>
                        <p v-if="!isUsuarioValid && isUsuarioValid != null" :class="{'invalid-label' : !isUsuarioValid}">Debe asignar un usuario</p>
                    </div>
                    </CCol>
                    </CRow>
                </CCardBody>
                    <CCardFooter>
                        <CButton type="submit" color="primary" @click.prevent="createDoctor">Guardar</CButton>
                </CCardFooter>
                </CCard>
            </CCol>
            <CCol sm="8">
                <CCard>
            <CCardHeader>
                <strong>Lista de Doctores</strong>
            </CCardHeader>
            <CCardBody>
                <CDataTable
                    :fields="fields"
                    :items="doctores"
                    table-filter
                    items-per-page-select
                    :items-per-page="10"
                    hover
                    sorter
                    pagination
                    >
                    <template #editarDoctor="{item}">
                        <td class="py-2">
                            <CButton
                                color="primary"
                                variant="outline"
                                square
                                size="sm"
                                @click.prevent="updateDoctor(item)"
                            >
                                Modificar
                            </CButton>
                        </td>
                    </template>
                    </CDataTable>
                </CCardBody>
            </CCard>
            </CCol>
        </CRow>
    </div>
</template>

<script>
import DoctorService from '../../services/doctor-service'
import UsuarioService from '../../services/usuario-service'
import vSelect from 'vue-select'
import 'vue-select/dist/vue-select.css'
import { required, requiredUnless } from 'vuelidate/lib/validators'

const fields = [
    { key: 'nombre' },
    { key: 'apellido' },
    { key: 'guid' },
    { key: 'posicion' },
    { key: 'nombreUsuario', label: 'Usuario' },
    { key: 'editarDoctor', label: 'Editar', _style: 'width:1%', sorter: false, filter: false }
]
export default {
    name: 'doctor',
    components: {
        vSelect
    },
    data () {
        return {
            doctores: [],
            usuarios: [],
            doctor: {
                nombre: '',
                apellido: '',
                posicion: '',
                usuarioId: 0
            },
            fields,
            message: '',
            alert: false,
            isUsuarioValid: null
        }
    },
    validations: {
        doctor: {
            nombre: { required },
            apellido: { required },
            posicion: { required },
            usuarioId: {
                required: requiredUnless(vm => {
                    return vm.usuarioId.$model === 0
                })
            }
        }
    },
    mounted () {
        this.getDoctores()
        this.getUsuarios()
    },
    methods: {
        getDoctores () {
            DoctorService.getDoctores().then(
                response => {
                    const data = response.data
                    for (const key in data) {
                        const doctor = data[key]
                        doctor.nombreUsuario = doctor.usuario.nombreUsuario
                        this.doctores.push(doctor)
                    }
                }
            )
        },
        getUsuarios () {
            UsuarioService.getUsuarios().then(
                response => {
                    const data = response.data
                    for (const key in data) {
                        const usuario = data[key]
                        usuario.id = key
                        this.usuarios.push(usuario)
                    }
                }
            )
        },
        createDoctor () {
            this.$v.$touch()
            if (this.$v.doctor.usuarioId.$model === 0) {
                this.isUsuarioValid = false
            }
            if (this.$v.$invalid) {
                return
            }
            const doctorData = {
                nombre: this.doctor.nombre,
                apellido: this.doctor.apellido,
                posicion: this.doctor.posicion,
                usuarioId: this.doctor.usuarioId.usuarioId
            }
            console.log(doctorData)
            DoctorService.createDoctor(doctorData).then(
                response => {
                    const data = response.data
                    if (data.error) {
                        this.alert = true
                        this.message = data.error
                        return this.message
                    }

                    console.log(data)
                    this.alert = false
                    const doctor = data
                    doctor.nombreUsuario = doctor.usuario.nombreUsuario
                    doctor.usuarioId = doctor.usuario.usuarioId

                    if (doctor != null) {
                        this.doctores.push(doctor)
                        this.$v.$reset()
                        this.clear()
                    }
                }
            )
        },
        checkUsuarioValue (event) {
            if (event === null) {
                this.isUsuarioValid = false
            } else {
                this.isUsuarioValid = true
            }
        },
        clear () {
            this.doctor.nombre = ''
            this.doctor.apellido = ''
            this.doctor.posicion = ''
            this.doctor.usuario = ''
        }
    }
}
</script>

<style scoped>
    .vs--disabled .vs__clear, .vs--disabled .vs__dropdown-toggle, .vs--disabled .vs__open-indicator, .vs--disabled .vs__search, .vs--disabled .vs__selected {
        cursor: auto;
        background-color: #d8dbe0;
    }

    .invalid {
        border: 1px solid #e55353;
    }

    .invalid-label {
        width: 100%;
        margin-top: 0.25rem;
        font-size: 80%;
        color: #e55353
    }
</style>
