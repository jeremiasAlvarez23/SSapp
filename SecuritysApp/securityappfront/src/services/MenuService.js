// src/services/MenuService.js

const apiBaseUrl = 'http://localhost:5187';

const getToken = () => localStorage.getItem('token');

const headers = () => ({
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${getToken()}`
});

export default {
    async obtenerPorUsuario() {
        const response = await fetch(`${apiBaseUrl}/api/v1/menu/usuario`, {
            method: 'GET',
            headers: headers()
        });
        return response.ok ? response.json() : Promise.reject(await response.json());
    },

    async obtenerTodo() {
        const response = await fetch(`${apiBaseUrl}/api/v1/menu/todo`, {
            method: 'GET',
            headers: headers()
        });
        return response.ok ? response.json() : Promise.reject(await response.json());
    },

    async obtenerPadres() {
        const response = await fetch(`${apiBaseUrl}/api/v1/menu/padres`, {
            method: 'GET',
            headers: headers()
        });
        return response.ok ? response.json() : Promise.reject(await response.json());
    },

    async obtenerUltimoOrden(padreId) {
        const response = await fetch(`${apiBaseUrl}/api/v1/menu/ultimo-orden/${padreId}`, {
            method: 'GET',
            headers: headers()
        });
        return response.ok ? response.json() : Promise.reject(await response.json());
    },

    async insertar(data) {
        const response = await fetch(`${apiBaseUrl}/api/v1/menu/insertar`, {
            method: 'POST',
            headers: headers(),
            body: JSON.stringify(data)
        });
        return response.ok ? response.json() : Promise.reject(await response.json());
    },

    async editar(id, data) {
        const response = await fetch(`${apiBaseUrl}/api/v1/menu/editar`, {
            method: 'PUT',
            headers: headers(),
            body: JSON.stringify({ id, ...data })
        });
        return response.ok ? response.json() : Promise.reject(await response.json());
    },

    async eliminar(id) {
        const response = await fetch(`${apiBaseUrl}/api/v1/menu/eliminar/${id}`, {
            method: 'DELETE',
            headers: headers()
        });
        return response.ok ? response.json() : Promise.reject(await response.json());
    }
};
