// Define a URL base da sua API
const API_URL = 'http://localhost:5085'; // Ajuste a porta se for diferente

// Pega o ID do usuário "logado"
const usuarioId = localStorage.getItem('usuarioId');
let myChart; // Variável para guardar a instância do gráfico

// Verifica se o usuário está "logado", se não, volta para o cadastro
if (!usuarioId) {
    window.location.href = 'index.html';
}

// Evento de "submit" do formulário de lançamento
document.getElementById('form-lancamento').addEventListener('submit', async (event) => {
    event.preventDefault();
    
    // Coleta os dados do formulário
    const lancamento = {
        descricao: document.getElementById('descricao').value,
        valor: parseFloat(document.getElementById('valor').value),
        tipo: document.getElementById('tipo').value,
        categoria: document.getElementById('categoria').value,
        data: new Date().toISOString(),
        usuarioId: parseInt(usuarioId)
    };

    try {
        // 1. Chama o endpoint de registro de lançamento
        const response = await fetch(`${API_URL}/api/lancamentos`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(lancamento)
        });

        if (!response.ok) throw new Error('Falha ao registrar lançamento');
        
        // Limpa o formulário e atualiza o dashboard
        document.getElementById('form-lancamento').reset();
        carregarDashboard();

    } catch (error) {
        console.error('Erro ao registrar:', error);
        alert('Não foi possível registrar o lançamento.');
    }
});

// Função para carregar todos os dados (saldo, lista e gráfico)
async function carregarDashboard() {
    try {
        const response = await fetch(`${API_URL}/api/lancamentos/por-usuario/${usuarioId}`);
        if (!response.ok) throw new Error('Falha ao buscar dados');
        
        const lancamentos = await response.json();
        
        // Funções para validar o escopo da PoC
        calcularEExibirSaldo(lancamentos);
        exibirListaLancamentos(lancamentos);
        desenharGrafico(lancamentos);

    } catch (error) {
        console.error('Erro ao carregar dashboard:', error);
    }
}

// Atende ao critério: "Cálculo e exibição do saldo total"
function calcularEExibirSaldo(lancamentos) {
    let saldo = 0;
    lancamentos.forEach(l => {
        if (l.tipo === 'Receita') {
            saldo += l.valor;
        } else {
            saldo -= l.valor;
        }
    });
    
    document.getElementById('saldo-total').innerText = `Saldo Total: R$ ${saldo.toFixed(2)}`;
}

// Atende ao critério: "Visualização de um gráfico simples"
function desenharGrafico(lancamentos) {
    const ctx = document.getElementById('grafico-gastos').getContext('2d');
    
    // Agrupa gastos por categoria
    const gastosPorCategoria = {};
    lancamentos.filter(l => l.tipo === 'Despesa').forEach(gasto => {
        gastosPorCategoria[gasto.categoria] = (gastosPorCategoria[gasto.categoria] || 0) + gasto.valor;
    });

    const labels = Object.keys(gastosPorCategoria);
    const data = Object.values(gastosPorCategoria);

    // Destrói o gráfico anterior se ele existir (para atualizar)
    if (myChart) {
        myChart.destroy();
    }

    myChart = new Chart(ctx, {
        type: 'pie', // Gráfico de pizza simples
        data: {
            labels: labels,
            datasets: [{
                label: 'Gastos por Categoria',
                data: data,
                backgroundColor: ['#FF6384', '#36A2EB', '#FFCE56', '#4BC0C0', '#9966FF']
            }]
        }
    });
}

// Função bônus para exibir a lista
function exibirListaLancamentos(lancamentos) {
    const lista = document.getElementById('lista-lancamentos');
    lista.innerHTML = ''; // Limpa a lista
    
    // Pega os 5 últimos
    lancamentos.slice(-5).reverse().forEach(l => {
        const item = document.createElement('li');
        const valorClasse = l.tipo === 'Receita' ? 'receita' : 'despesa';
        item.innerHTML = `${l.descricao} (${l.categoria}) <span class="${valorClasse}">R$ ${l.valor.toFixed(2)}</span>`;
        lista.appendChild(item);
    });
}

// Carrega os dados assim que a página é aberta
document.addEventListener('DOMContentLoaded', carregarDashboard);
