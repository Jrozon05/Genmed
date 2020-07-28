<template>
    <div>
        <CRow>
            <CCol sm="12">
                <CCard>
            <CCardHeader>
                <strong>Listado de Posiciones</strong>
            </CCardHeader>
            <CCardBody>
            <CDataTable
                :fields="fields"
                :items="usuarios"
                table-filter
                items-per-page-select
                :items-per-page="10"
                hover
                sorter
                pagination
                >
                <template #editarUsuario="{item}">
                    <td class="py-2">
                        <CButton
                            color="primary"
                            variant="outline"
                            square
                            size="sm"
                            @click.prevent="updateUsuario(item)"
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
export default {
}
</script>
