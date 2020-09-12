<template>
    <div>
            <CRow>
                <CCol sm="12">
                    <CCard>
                    <CCardHeader>
                        <strong>Configuración</strong>
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
                        label="Contraseña"
                        placeholder="Introduzca su constraseña nueva"
                        type="password"
                        v-model="usuario.clave"
                        @input="$v.usuario.clave.$touch()"
                        :invalid-feedback="!$v.usuario.clave.isClaveValido ? 'La clave debe tener al menos 6 letras (solo acepta letras y números).' : 'La clave es un campo requerido'"
                        :is-valid="!$v.usuario.clave.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Confirmar Contraseña"
                        placeholder="Confirmar su constraseña nueva"
                        type="password"
                        v-model="usuario.confirmarClave"
                        @input="$v.usuario.confirmarClave.$touch()"
                        :invalid-feedback="!$v.usuario.confirmarClave.sameAsClave ? 'Las contraseñas deben ser idénticas' : 'la confirmación de contraseña es un campo requerido (solo acepta letras y números)'"
                        :is-valid="!$v.usuario.confirmarClave.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    </CCardBody>
                    <CCardFooter>
                        <CButton type="submit" color="success" @click.prevent="UpdateClave">Salvar</CButton>&nbsp;
                        <!-- <CButton type="submit" variant="outline" color="dark" @click.prevent="CancelarForm">Cancelar</CButton> -->
                    </CCardFooter>
                </CCard>
                </CCol>
            </CRow>
    </div>
</template>

<script>
import UsuarioService from '../../services/usuario-service'
import { required, sameAs, helpers } from 'vuelidate/lib/validators'

const isClaveValido = helpers.regex('clave', /^(((?=.*[a-z])(?=.*[A-Z]))|((?=.*[a-z])(?=.*[0-9]))|((?=.*[A-Z])(?=.*[0-9])))(?=.{6,})/)

export default {
  name: 'Table',
  data () {
      return {
          usuario: {
              clave: '',
              confirmarClave: ''
          },
          message: '',
          alert: false
      }
  },
  validations: {
      usuario: {
          clave: {
              required,
              isClaveValido
          },
          confirmarClave: {
              required,
              sameAsClave: sameAs('clave'),
              isClaveValido
          }
      }
  },
  methods: {
    UpdateClave () {
        const usuarioData = {
            guid: this.$route.params.guid,
            clave: this.usuario.clave,
            confirmarClave: this.usuario.confirmarClave
        }

        UsuarioService.UpdateClave(usuarioData).then(
            response => {
                const data = response.data
                
                if (data.error) {
                this.alert = true
                this.message = data.error
                return this.message
            }
            this.alert = false
                if (data.flag) {
                    this.$alertify.success('Clave Salvado...')
                } else {
                    this.$alertify.warning('Error en salvar clave')
                }
            }
        )
    },
    CancelarForm () {
        this.$router.push({ name: 'Usuario' })
    },
    success () {
        this.$alertify.success('success')
    },
    warning () {
        this.$alertify.warning('warning')
    }
  }
}
</script>
