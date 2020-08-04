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
                    @input="$v.paciente.nombre.$touch()"
                    invalid-feedback="El nombre del paciente es un campo requerido"
                    :is-valid="!$v.paciente.nombre.$error ? null : false"
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
                    v-model="paciente.edad"
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
                    v-model="direccion.direccionActual"
                    />
                </CCol>
                <CCol sm="6">
                    <CInput
                    label="Lugar de Nacimiento"
                    placeholder="Introduzca el lugar de nacimiento"
                    v-model="direccion.lugarNacimiento"
                    />
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <div class="form-group">
                        <label>Pais</label>
                        <select class="form-control" v-model="direccion.pais" disabled>
                            <option value="0">Seleccionar el pais</option>
                            <option value="1" Selected>Republica Dominicana</option>
                        </select>
                    </div>
                </CCol>
                <CCol sm="6">
                    <div class="form-group">
                        <label>Ciudad</label>
                        <v-select label="countryName" :options="ciudades" v-model="direccion.ciudad" placeholder="Seleccionar la ciudad"></v-select>
                    </div>
                </CCol>
            </CRow>
            <CRow>
                <CCol sm="6">
                    <div class="form-group">
                        <label>Provincia</label>
                        <v-select label="countryName" :options="ciudades" v-model="direccion.provincia" placeholder="Selecciona la provincia"></v-select>
                    </div>
                </CCol>
                <CCol sm="6">
                    <div class="form-group">
                        <label>Sector</label>
                        <v-select label="countryName" :options="ciudades" v-model="direccion.sector" placeholder="Selecciona el sector"></v-select>
                    </div>
                </CCol>
            </CRow>
            </CCol>
        </CRow>
        <hr>
    </div>
</template>

<script>
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
              sexo: '',
              estadoCivil: 0,
              cedula: '',
              email: '',
              phone: '',
              phoneAlternativo: ''
          },
          direccion: {
              direccionActual: '',
              lugarNacimiento: '',
              pais: '',
              ciudad: '',
              provincia: '',
              sector: ''
          },
          ciudades: [
            {
                countryCode: '1',
                countryName: 'Santiago'
            },
            {
                countryCode: '2',
                countryName: 'Distrito Nacional'
            }
        ]
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
        validate () {
            this.$v.$touch()
            var isValid = !this.$v.$invalid
            this.$emit('on-validate', this.$data, isValid)
            return isValid
        }
    }
  }
</script>
