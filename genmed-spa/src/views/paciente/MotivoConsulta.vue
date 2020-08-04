<template>
    <div>
        <CAlert color="dark">
            <strong>MOTIVO DE CONSULTA</strong>
        </CAlert>
        <editor v-model="value" @change="onChange" placeholder="" theme="snow"></editor>
    </div>
</template>

<script>
export default {
        props: ['pacientes'],
        data () {
            return {
                value: ''
            }
        },
        methods: {
            val () {
                this.value = "This's new value"
            },
            onChange (html, text) {
                console.log(html.length, text)
            }
        }
    }
</script>

<style>
    .ql-editor, .ql-blank {
        min-height: 350px;
    }
</style>
