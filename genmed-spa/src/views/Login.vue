<template>
  <div class="d-flex align-items-center min-vh-100">
    <CContainer fluid>
      <CRow class="justify-content-center">
        <CCol md="4">
          <CCard class="mx-4 mb-0">
            <CCardBody class="p-4">
              <CForm>
                <h1>Login</h1>
                <p class="text-muted">Iniciar sesión en su cuenta</p>
                <CInput placeholder="Nombre Usuario" autocomplete="nombreusuario">
                  <template #prepend-content>
                    <CIcon name="cil-user" />
                  </template>
                </CInput>
                <CInput placeholder="Contraseña" type="password" autocomplete="curent-password">
                  <template #prepend-content>
                    <CIcon name="cil-lock-locked" />
                  </template>
                </CInput>
                <CRow>
                  <CCol col="6" class="text-left">
                    <CButton color="primary" class="px-4">Login</CButton>
                  </CCol>
                  <CCol col="6" class="text-right">
                    <CButton color="link" class="px-0">Se te olvidó tu contraseña?</CButton>
                  </CCol>
                </CRow>
              </CForm>
            </CCardBody>
          </CCard>
        </CCol>
      </CRow>
    </CContainer>
  </div>
</template>

<script>
export default {
  name: 'Login'
}
</script>
