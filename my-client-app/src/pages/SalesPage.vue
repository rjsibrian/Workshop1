<template>
	<div class="sales-page">
		<loader-comp :loading="state.loading" />
		<h1>Sales Overview</h1>
		<p class="left-align"> Here you can view all sales made in the company over the years associated with a seller. You can filter the search by the seller's name and a specific year. </p>
		<search-box
			@searchByName="searchByNameAndYear"
			@searchByYear="searchByNameAndYear"
		/>
		<sale-table :sales="state.sales" />
	</div>
</template>

<script setup>
	import { onMounted, reactive } from "vue";
	import SaleTable from "@/components/SaleTable.vue";
	import SearchBox from "@/components/SearchBoxSale.vue";
	import axios from "/src/utils/axios.js";
	import LoaderComp from "@/components/LoaderComp.vue";

	const state = reactive({
		loading: false,
		sales: [],
		originalSales: []
	});

	async function fetchData() {
		try {
			state.loading = true;
			const response = await axios.get("/api/sales");
			state.sales = response.data;
			state.sales.sort((a, b) => a.salesPersonID  - b.salesPersonID );
		} catch (error) {
			console.error(error);
		} finally {
			state.loading = false;
		}
	}

	async function searchByNameAndYear(name, year) {
		try {
			state.loading = true;
			let response;

			if (name && year && year !== "All") {
				response = await axios.get(
					`api/sales/ByNameAndYear/${name}/${year}`
				);
			} else if (name) {
				response = await axios.get(`api/sales/ByName/${name}`);
			} else if (year && year !== "All") {
				response = await axios.get(`api/sales/ByYear/${year}`);
			} else {
				response = await axios.get("api/sales");
			}

			state.sales = response.data;
		} catch (error) {
			if (error.response && error.response.status === 404) {
				state.sales = [];
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
	.sales-page {
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