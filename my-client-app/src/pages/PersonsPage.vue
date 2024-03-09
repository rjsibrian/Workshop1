<template>
	<div class="persons-page">
		<loader-comp :loading="state.loading" />
		<h1>List of Persons</h1>
        <p class="left-align"> Here you can view all the company's personnel here, and you can filter by a specific name or by person type. </p>
		<search-box
			@searchByName="searchByNameAndType"
			@searchByType="searchByNameAndType"
		/>
		<person-table :persons="state.persons" />
	</div>
</template>

<script setup>
	import { onMounted, reactive } from "vue";
	import PersonTable from "@/components/PersonTable.vue";
	import SearchBox from "@/components/SearchBoxPerson.vue";
	import axios from "/src/utils/axios.js";
	import LoaderComp from "@/components/LoaderComp.vue";

	const state = reactive({
		loading: false,
		persons: [],
	});

	async function fetchData() {
		try {
			state.loading = true;
			const response = await axios.get("/api/persons");
			state.persons = response.data;
			state.persons.sort((a, b) => a.businessEntityID - b.businessEntityID);
		} catch (error) {
			console.error(error);
		} finally {
			state.loading = false;
		}
	}

	async function searchByNameAndType(name, type) {
		try {
			state.loading = true;
			let response;
			if (name && type && type !== "None") {
				response = await axios.get(
					`api/persons/ByNameAndPersonType/${name}/${type}`
				);
			} else if (name) {
				response = await axios.get(`api/persons/ByName/${name}`);
			} else if (type && type !== "None") {
				response = await axios.get(`api/persons/ByPersonType/${type}`);
			} 
            else {
				response = await axios.get("api/persons");
			}
			state.persons = response.data;
		} catch (error) {
			if (error.response && error.response.status === 404) {
				state.persons = [];
			} else {
				console.error(error);
			}
		} finally {
			state.loading = false;
		}
	}

	onMounted(async () => {
		await fetchData();
	});
</script>
<style scoped>
	.persons-page {
		width: 75%;
		margin-top: 80px;
		margin-bottom: 50px;
		display: flex;
		flex-direction: column;
		align-items: flex-start;
	}
    .left-align {
        text-align: left;
    }
</style>
