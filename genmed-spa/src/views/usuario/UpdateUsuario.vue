<template>
    <div>
        <CRow>
            <CCol sm="5">
            <CCard>
                <CCardHeader>
                    <strong>Editar Usuario </strong>
                </CCardHeader>
                <CCardBody>
                    <CRow>
                        <CCol sm="12">
                            <CInput
                            label="Identificación"
                            placeholder="Introduzca su nombre de usuario"
                            disabled
                            v-model="usuario.guid"
                            />
                        </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Nombre Usuario"
                        placeholder="Introduzca su nombre de usuario"
                        @input="$v.usuario.nombreUsuario.$touch()"
                        v-model="usuario.nombreUsuario"
                        :invalid-feedback="!$v.usuario.nombreUsuario.isNombreUsuarioValido ? 'El nombre de usuario solo acepta letras y número' : 'El nombre de usuario es un campo requerido'"
                        :is-valid="!$v.usuario.nombreUsuario.$error ? null : false"
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <CInput
                        label="Correo Electronico"
                        @input="$v.usuario.email.$touch()"
                        v-model="usuario.email"
                        :invalid-feedback="!$v.usuario.email.email ? 'No tiene un formato de correo electronico' : 'El correo electronico es un campo requerido'"
                        :is-valid="!$v.usuario.email.$error ? null : false"
                        disabled
                        />
                    </CCol>
                    </CRow>
                    <CRow>
                    <CCol sm="12">
                        <div class="form-group">
                            <label for="">Roles</label>
                            <select class="form-control" v-model="usuario.rolId" @change="checkRolValue($event)" :class="{ 'invalid' : !isRolValid && isRolValid != null }">
                                <option value="1">Super Admin</option>
                                <option value="2">Admin</option>
                                <option value="3">Usuario</option>
                                <option value="4">Solo Lectura</option>
                            </select>
                            <p v-if="!isRolValid && isRolValid != null" :class="{'invalid-label' : !isRolValid}">Debe asignar un rol</p>
                        </div>
                    </CCol>
                    </CRow>
                </CCardBody>
                <CCardFooter>
                        <CButton type="submit" color="success" @click.prevent="UpdateUsuario">Salvar</CButton>&nbsp;
                        <CButton type="submit" variant="outline" color="dark" @click.prevent="CancelarForm">Cancelar</CButton>
                        <CButton v-if="Boolean(!isActive)" type="submit" variant="outline" :color="Boolean(isActive) ? 'danger' : 'success'" @click="ActivarUsuario" class="btnFloat">{{Boolean(isActive) ? 'Desactivar' : 'Activar'}}</CButton>
                        <CButton v-if="Boolean(isActive)" type="submit" variant="outline" :color="Boolean(isActive) ? 'danger' : 'success'" @click="DeleteUsuario" class="btnFloat">{{Boolean(isActive) ? 'Desactivar' : 'Activar'}}</CButton>
                </CCardFooter>
            </CCard>
        </CCol>
        <CCol sm="6">
            <UpdateClave></UpdateClave>
        </CCol>
        </CRow>
    </div>
</template>

<script>
import UsuarioService from '../../services/usuario-service'
import { required, email, requiredUnless, helpers } from 'vuelidate/lib/validators'
import UpdateClave from './UpdateClave'

const isNombreUsuarioValido = helpers.regex('nombreUsuario', /(?=[A-Za-z0-9])(?!._-]{1})[A-Za-z0-9._-]{3,15}$/)

export default {
  name: 'Table',
  data () {
      return {
          usuarios: [],
          doctores: [],
          usuario: {
              guid: '',
              nombreUsuario: '',
              email: '',
              rolId: 0
          },
          message: '',
          alert: false,
          isRolValid: null,
          isActive: true
      }
  },
  components: {
      UpdateClave
  },
  validations: {
      usuario: {
          nombreUsuario: {
              required,
              isNombreUsuarioValido
            },
          email: {
              required,
              email
          },
          rolId: {
              required: requiredUnless(vm => {
                  return vm.rolId.$model === 0
              })
          }
      }
  },
  computed: {
      guidToEdit () {
         return this.$route.params.guid
      }
  },
  created () {
      this.changeUsuarioInfo(this.$route.params.guid)
  },
  watch: {
      guidToEdit () {
          this.changeUsuarioInfo(this.guidToEdit)
      }
  },
  methods: {
    changeUsuarioInfo (item) {
        UsuarioService.getUsuarioByGuid(item).then(
            response => {
                const data = response.data
                const usuario = data
                this.isActive = usuario.activo
                this.usuario.nombreUsuario = usuario.nombreUsuario
                this.usuario.email = usuario.email
                this.usuario.rolId = usuario.rol.rolId
                this.usuario.guid = usuario.guid
            }
        )
    },
    UpdateUsuario () {
        this.$v.$touch()
        if (this.$v.usuario.rolId.$model === 0) {
            this.isRolValid = false
        }
        if (this.$v.$invalid) {
            return
        }
        const usuarioData = {
            guid: this.usuario.guid,
            nombreUsuario: this.usuario.nombreUsuario,
            email: this.usuario.email,
            rolId: parseInt(this.usuario.rolId),
            activo: this.isActive
        }

        UsuarioService.UpdateUsuario(usuarioData).then(
            response => {
                const data = response.data
                if (data.error) {
                    this.alert = true
                    this.message = data.error
                    return this.message
                }
                this.alert = false
                const usuario = data
                usuario.usuarioId = data.usuarioId
                usuario.email = data.email
                usuario.rol = usuario.rol.nombre

                if (usuario != null) {
                    this.$alertify.success('Usuario Salvado...')
                } else {
                    this.$alertify.warning('Error en salvar el usuario')
                }
            }
        )
    },
    DeleteUsuario () {
        UsuarioService.DeactivateUsuario(this.guidToEdit).then(
            response => {
                const data = response.data
                this.isActive = data.flag
            }
        )
    },
    ActivarUsuario () {
        UsuarioService.ActivateUsuario(this.guidToEdit).then(
            response => {
                const data = response.data
                this.isActive = data.flag
            }
        )
    },
    checkRolValue () {
        if (event.target.value === '0') {
            this.isRolValid = false
        } else {
            this.isRolValid = true
        }
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

<style scoped>
    .btnFloat {
        float: right;
    }
</style>
