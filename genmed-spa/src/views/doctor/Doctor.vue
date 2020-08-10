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
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Apellido(s)"
                        placeholder="Introduzca su apellido(s)"
                        v-model="doctor.apellido"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Posición"
                        placeholder="Introduzca su posición"
                        v-model="doctor.posicion"
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
                            v-model="doctor.usuario"
                            placeholder="Seleccionar el usuario">
                        </v-select>
                    </div>
                    </CCol>
                    </CRow>
                </CCardBody>
                    <CCardFooter>
                        <CButton type="submit" color="primary" @click.prevent="createUsuario">Guardar</CButton>
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
                usuario: ''
            },
            fields,
            message: '',
            alert: false
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
        }
    }
}
</script>

<style>
    .vs--disabled .vs__clear, .vs--disabled .vs__dropdown-toggle, .vs--disabled .vs__open-indicator, .vs--disabled .vs__search, .vs--disabled .vs__selected {
        cursor: auto;
        background-color: #d8dbe0;
    }
</style>
