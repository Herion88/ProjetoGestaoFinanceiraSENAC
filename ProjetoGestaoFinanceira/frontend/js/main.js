
// Define a URL base da sua API
const API_URL = 'http://localhost:5085'; // Ajuste a porta se for diferente

document.getElementById('form-cadastro').addEventListener('submit', async (event) => {
    event.preventDefault();

    const nome = document.getElementById('nome').value;
    const email = document.getElementById('email').value;

    try {
        // 1. Chama o endpoint de cadastro no backend
        const response = await fetch(`${API_URL}/api/usuarios`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ nome, email })
        });
        
        if (!response.ok) throw new Error('Falha no cadastro');
        
        const usuarioCriado = await response.json();

        // 2. Salva o ID do usuário no navegador (localStorage)
        // Isso simula o "login" para a PoC
        localStorage.setItem('usuarioId', usuarioCriado.id);
        
        // 3. Redireciona para o dashboard
        window.location.href = 'dashboard.html';

    } catch (error) {
        console.error('Erro ao cadastrar:', error);
        alert('Não foi possível realizar o cadastro.');
    }
});
