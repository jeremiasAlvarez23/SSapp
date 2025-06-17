const apiBaseUrl = 'http://localhost:5187';

export default {
  async login(email, clave) {
    const response = await fetch(`${apiBaseUrl}/api/v1/login`, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email, clave })
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || 'Error al iniciar sesión');
    }

    return response.json();
  },

  async logout() {
    const token = localStorage.getItem('token');
    const response = await fetch(`${apiBaseUrl}/api/v1/logout`, {
      method: 'POST',
      headers: { 'Authorization': `Bearer ${token}` }
    });

    if (!response.ok) {
      const error = await response.json();
      throw new Error(error.message || 'Error al cerrar sesión');
    }

    // Limpiar almacenamiento local
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
  }
}
