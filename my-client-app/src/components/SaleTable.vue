<template>
	<table v-if="state.sales && state.sales.length > 0" class="box-shadow">
		<thead>
			<tr>
                <th>SalesPersonID</th>
				<th>Name</th>
				<th>OrderYear</th>
				<th>SalesOrderID</th>
                <th>TotalDue</th>
			</tr>
		</thead>
		<tbody>
			<tr v-for="sale in paginatedSales" :key="sale.rowID">
                <td>{{ sale.salesPersonID }}</td>
                <td>{{ sale.firstName }} {{ sale.lastName }}</td>
                <td>{{ sale.orderYear }}</td>
                <td>{{ sale.salesOrderID }}</td>
                <td>{{ sale.totalDue }}</td>
			</tr>
		</tbody>
		<tfoot>
			<tr>
				<td colspan="5">
					<div class="pagination">
						<button
							@click="goToPage(state.currentPage - 1)"
							:disabled="state.currentPage === 1"
							style="border: 0"
						>
							&lt;
						</button>
						<span
							v-for="page in visiblePages"
							:key="page"
							@click="goToPage(page)"
							:class="{ active: page === state.currentPage }"
							class="page-number"
						>
							{{ page }}
						</span>
						<button
							@click="goToPage(state.currentPage + 1)"
							:disabled="state.currentPage === totalPages"
							style="border: 0"
						>
							&gt;
						</button>
					</div>
				</td>
			</tr>
		</tfoot>
	</table>
	<table v-else class="box-shadow">
		<thead>
			<tr>
                <th>SalesPersonID</th>
				<th>Name</th>
				<th>OrderYear</th>
				<th>SalesOrderID</th>
                <th>TotalDue</th>
			</tr>
		</thead>
		<tbody>
			<tr>
				<td colspan="5">No results found.</td>
			</tr>
		</tbody>
	</table>
</template>
<script setup>
	import { computed, reactive, defineProps } from "vue";

	const props = defineProps({
		sales: Array,
	});

	const state = reactive({
		sales: computed(() => props.sales),
		pageSize: 20,
		currentPage: 1,
	});

	const paginatedSales = computed(() => {
		const startIndex = (state.currentPage - 1) * state.pageSize;
		const endIndex = startIndex + state.pageSize;
		return state.sales.slice(startIndex, endIndex);
	});

	const totalPages = computed(() =>
		Math.ceil(state.sales.length / state.pageSize)
	);

	const visiblePages = computed(() => {
		const range = 4;
		const start = Math.max(1, state.currentPage - range);
		const end = Math.min(totalPages.value, start + range * 2);

		const result = [];
		for (let i = start; i <= end; i++) {
			result.push(i);
		}
		return result;
	});

	function goToPage(page) {
		state.currentPage = page;
	}
</script>
<style>
	table {
		width: 100%;
		border-spacing: 0;
		border: 2px solid #ddd;
		border-radius: 8px;
		overflow: hidden;
		align-self: flex-start;
	}

	th,
	td {
		border-bottom: 1px solid #ddd;
		padding: 8px;
		text-align: left;
	}

	th {
		background-color: #f2f2f2;
	}

	tfoot {
		background-color: #f2f2f2;
	}

	.pagination {
		margin-top: 10px;
		display: flex;
		justify-content: center;
		align-items: center;
	}

	button {
		cursor: pointer;
		padding: 8px;
	}

	.page-number {
		padding: 8px;
		cursor: pointer;
	}

	.page-number.active {
		background-color: #181723;
		color: #fff;
		border-color: #181723;
		border-radius: 4px;
	}
</style>