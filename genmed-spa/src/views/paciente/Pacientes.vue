<template>
    <div>
        <CRow>
            <CCol sm="12">
                <CCard>
            <CCardHeader>
                <strong>Lista de Pacientes</strong>
                <div style="float: right;">
                    <CButton
                            color="success"
                            square
                            size="sm"
                            @click.prevent="updateUsuario()"
                        >
                            Crear Paciente
                        </CButton>
                </div>
            </CCardHeader>
            <CCardBody>
            <CDataTable
                table-filter
                items-per-page-select
                :items-per-page="10"
                hover
                sorter
                pagination
                >
                </CDataTable>
            </CCardBody>
        </CCard>
            </CCol>
        </CRow>
    </div>
</template>

<script>
export default {
  name: 'Table',
  methods: {
    updateUsuario () {
        this.$router.push({ name: 'Paciente / Create Paciente' })
    }
  }
}
</script>

<style scoped>
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
