<template>
  <div class="mainCmp">
    <DataTable :value="arr" showGridlines scrollable scroll-height="70vmin">
      <Column field="id" header="Id" :style="{width: '25vmin'}" />
      <Column field="value" header="Value" :style="{width: '25vmin'}" />
    </DataTable>
    <Button label="+" @click="visible = true" />
    <Dialog
      v-model:visible="visible"
      modal
      header="Добавление нового элемента"
      :style="{ width: '50vmin' }"
    >
      <InputText type="text" v-model="newValue" placeholder="Новое значение" />
      <template #footer>
        <Button
          label="Отмена"
          icon="pi pi-times"
          @click="visible = false"
          text
        />
        <Button
          label="Добавить"
          icon="pi pi-check"
          @click="
            () => {
              visible = false;
              addNewValue();
            }
          "
          autofocus
        />
      </template>
    </Dialog>
  </div>
</template>

<script lang="js">
import "./style.scss"
import { defineComponent } from 'vue';
import DataTable from 'primevue/datatable'
import Column from 'primevue/column'
import Button from "primevue/button";
import Dialog from "primevue/dialog";
import InputText from "primevue/inputtext";

export default defineComponent({
    components: {
        DataTable,
        Column,
        Button,
        Dialog,
        InputText
    },
    data() {
        return {
            newValue: '',
            visible: false,
            arr:[]
        };
    },
    created() {

    },
    mounted(){
        this.fetchData();
    },
    watch: {

    },
    methods: {
        fetchData() {
            fetch('/data/read')
                .then(r => r.json())
                .then(json => {
                    this.arr = json;
                    return;
                });
        },
        addNewValue(){
            console.log(this.newValue)
            fetch('/data/write', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(this.newValue)
            })
            .then(response => response.json())
            .then(json => {
                console.log(json);
                this.fetchData();
                this.newValue = "";
            })
            .catch(error => {
                console.error('Ошибка нового значения:', error);
            });
        }
    },
});
</script>
