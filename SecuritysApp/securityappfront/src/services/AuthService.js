const apiBaseUrl = 'http://localhost:5187'; // Asegúrate que coincida con tu Swagger

export default {
  async login(email, clave) {
    try {
      const response = await fetch(`${apiBaseUrl}/api/v1/login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email, clave })
      });

      if (!response.ok) {
        const error = await response.json();
        throw new Error(error.message || 'Credenciales inválidas');
      }

      return await response.json(); // Devuelve el objeto con token, nombre, etc.
    } catch (error) {
      console.error('❌ Error en AuthService:', error.message);
      throw error;
    }
  }
}
