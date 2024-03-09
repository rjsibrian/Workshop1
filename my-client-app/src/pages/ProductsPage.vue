<template>
	<div class="products-page">
		<loader-comp :loading="state.loading" />
		<h1>List of Products</h1>
        <p class="left-align"> Here you can view the entire catalog of company products, and you can filter by a specific product name or by product category. </p>
		<search-box
			@searchByName="searchByNameAndType"
			@searchByType="searchByNameAndType"
		/>
		<product-table :products="state.products" />
	</div>
</template>

<script setup>
	import { onMounted, reactive } from "vue";
	import ProductTable from "@/components/ProductTable.vue";
	import SearchBox from "@/components/SearchBoxProduct.vue";
	import axios from "/src/utils/axios.js";
	import LoaderComp from "@/components/LoaderComp.vue";

	const state = reactive({
		loading: false,
		products: [],
	});

	async function fetchData() {
		try {
			state.loading = true;
			const response = await axios.get("/api/products");
			state.products = response.data;
			state.products.sort((a, b) => a.rowID - b.rowID);
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
					`api/products/ByNameAndCategoryType/${name}/${type}`
				);
			} else if (name) {
				response = await axios.get(`api/products/ByName/${name}`);
			} else if (type && type !== "None") {
				response = await axios.get(`api/products/ByCategoryType/${type}`);
			} 
            else {
				response = await axios.get("api/products");
			}
			state.products = response.data;
		} catch (error) {
			if (error.response && error.response.status === 404) {
				state.products = [];
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
	.products-page {
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
