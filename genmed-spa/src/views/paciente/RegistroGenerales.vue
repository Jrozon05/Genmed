<template>
    <div>
        <CRow>
            <CCol sm="6">
            <CRow>
                <CCol sm="12">
                    <CAlert color="dark">
                        <strong>REGISTRO DE DATOS GENERALES</strong>
                    </CAlert>
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <CInput
                    label="Nombre(s)"
                    placeholder="Introduzca su nombre(s)"
                    v-model="paciente.nombre"
                    />
                </CCol>
                <CCol sm="6">
                    <CInput
                    label="Apellido(s)"
                    placeholder="Introduzca su apellido(s)"
                    v-model="paciente.apellido"
                    />
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <CInput
                    label="Fecha Nacimiento"
                    type="date"
                    v-model="paciente.fechaNacimiento"
                    />
                </CCol>
                <CCol sm="6">
                    <CInput
                    label="Edad Actual (Calculado)"
                    disabled
                    v-model="edad"
                    />
                </CCol>
            </CRow>
            <CRow>
            <CCol sm="6">
                <div class="form-group">
                    <label for="">Género / Sexo</label>
                    <select class="form-control" v-model="paciente.sexo" disabled>
                        <option value="0">Seleccionar Género / Sexo</option>
                        <option value="1" selected>Femenino</option>
                        <option value="2">Masculino</option>
                    </select>
                </div>
            </CCol>
            <CCol sm="6">
                <div class="form-group">
                    <label for="">Estado Civil</label>
                    <select class="form-control" v-model="paciente.estadoCivil">
                        <option value="0" selected>Seleccionar el estado civil</option>
                        <option value="1">Soltero</option>
                        <option value="2">Casado</option>
                        <option value="2">Viudo</option>
                    </select>
                </div>
            </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <CInput
                    label="Cédula / Seguro Social"
                    placeholder="Introduzca el numero de indentificación"
                    v-model="paciente.cedula"
                    />
                </CCol>
                <CCol sm="6">
                    <CInput
                    label="Correo Electronico"
                    placeholder="Introduzca el correo electronico"
                    v-model="paciente.email"
                    />
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <label>Teléfono Principal</label>
                    <masked-input
                        type="text"
                        name="phone"
                        class="form-control"
                        v-model="paciente.phone"
                        :mask="['(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/]"
                        :guide="false"
                        placeholder="(###) ###-####">
                    </masked-input>
                </CCol>
                <CCol sm="6">
                    <label>Teléfono Alternativo</label>
                    <masked-input
                        type="text"
                        name="phone"
                        class="form-control"
                        v-model="paciente.phoneAlternativo"
                        :mask="['(', /[1-9]/, /\d/, /\d/, ')', ' ', /\d/, /\d/, /\d/, '-', /\d/, /\d/, /\d/, /\d/]"
                        :guide="false"
                        placeholder="(###) ###-####">
                    </masked-input>
                </CCol>
            </CRow>
            </CCol>
                <CCol sm="6">
                    <CRow>
                <CCol sm="12">
                    <CAlert color="dark">
                        <strong>REGISTRO DE DIRECCION</strong>
                    </CAlert>
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <CInput
                    label="Dirección Actual"
                    placeholder="Introduzca la dirección actual"
                    v-model="paciente.direccionActual"
                    />
                </CCol>
                <CCol sm="6">
                    <CInput
                    label="Lugar de Nacimiento"
                    placeholder="Introduzca el lugar de nacimiento"
                    v-model="paciente.lugarNacimiento"
                    />
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <div class="form-group">
                        <label>Pais</label>
                        <select class="form-control" v-model="paciente.pais" disabled>
                            <option value="0">Seleccionar el pais</option>
                            <option value="1" Selected>Republica Dominicana</option>
                        </select>
                    </div>
                </CCol>
                <CCol sm="6">
                    <div class="form-group">
                        <label>Provincia</label>
                        <v-select label="nombre"
                            :options="provincias"
                            :value="provincias.provinciaId"
                            @input="setCiudadByProvincia"
                            v-model="paciente.provincia"
                            placeholder="Selecciona la provincia">
                        </v-select>
                    </div>
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <div class="form-group">
                        <label>Ciudad</label>
                        <v-select label="nombre"
                        :options="ciudades"
                        :value="ciudades.ciudadId"
                        @input="setSectorByCiudad"
                        v-model="paciente.ciudad"
                        placeholder="Seleccionar la ciudad"
                        :disabled="isCiudadDisabled"></v-select>
                    </div>
                </CCol>
                <CCol sm="6">
                    <div class="form-group">
                        <label>Sector</label>
                        <v-select label="nombre"
                        :options="sectores"
                        v-model="paciente.sector"
                        placeholder="Selecciona el sector"
                        :disabled="isSectorDisabled"></v-select>
                    </div>
                </CCol>
            </CRow>
            </CCol>
        </CRow>
        <hr>
    </div>
</template>

<script>
import DireccionService from '../../services/direccion/direccion-service'
import MaskedInput from 'vue-text-mask'
import vSelect from 'vue-select'
import 'vue-select/dist/vue-select.css'
import { required } from 'vuelidate/lib/validators'

export default {
    name: 'name',
    components: {
        MaskedInput,
        vSelect
    },
    data () {
        return {
            paciente: {
                nombre: '',
                apellido: '',
                fechaNacimiento: '',
                edad: 0,
                sexo: 1,
                estadoCivil: 0,
                cedula: '',
                email: '',
                phone: '',
                phoneAlternativo: '',
                direccionActual: '',
                lugarNacimiento: '',
                pais: 1,
                ciudad: '',
                provincia: '',
                sector: ''
            },
            provincias: [],
            ciudades: [],
            sectores: [],
            isCiudadDisabled: true,
            isSectorDisabled: true
        }
    },
    mounted () {
        this.getProvincias()
    },
    computed: {
        edad () {
            const today = new Date()
            const fechaNacimiento = new Date(this.paciente.fechaNacimiento)
            const age = today.getFullYear() - fechaNacimiento.getFullYear()
            return age
        }
    },
    validations: {
        paciente: {
            nombre: {
                required
            }
        }
    },
    methods: {
        getProvincias () {
            DireccionService.getProvincias().then(
                response => {
                    const data = response.data
                    for (const key in data) {
                        const provincia = data[key]
                        provincia.id = provincia.provinciaId
                        this.provincias.push(provincia)
                    }
                }
            )
        },
        getCiudadesByProvincia (provinciaId) {
            DireccionService.getCiudades(provinciaId).then(
                response => {
                    const data = response.data
                    for (const key in data) {
                        const ciudad = data[key]
                        ciudad.id = ciudad.provinciaId
                        this.ciudades.push(ciudad)
                    }
                }
            )
        },
        getSectoresByCiudad () {
            DireccionService.getSectores().then(
                response => {
                    const data = response.data
                    for (const key in data) {
                        const sector = data[key]
                        sector.id = sector.provinciaId
                        this.sectores.push(sector)
                    }
                }
            )
        },
        setCiudadByProvincia (provincia) {
            if (provincia == null) {
                this.ciudades = []
                this.isCiudadDisabled = true
                return this.isCiudadDisabled
            }
            this.ciudades = []
            this.isCiudadDisabled = false
            this.getCiudadesByProvincia(provincia.id)
        },
        setSectorByCiudad (ciudad) {
            this.sectores = []
            if (ciudad == null) {
                this.isSectorDisabled = true
                return this.isSectorDisabled
            }
            this.isSectorDisabled = false
            this.getSectoresByCiudad()
        },
        validate () {
            // this.$v.$touch()
            var isValid = true // !this.$v.$invalid
            if (isValid) {
                this.$store.dispatch('paciente/setRegistroGenerales', this.paciente)
            }
            return isValid
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
