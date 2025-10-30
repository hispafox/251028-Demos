# Ejemplos de Uso - TodoApp API

Este documento contiene ejemplos prácticos de cómo usar la API de TodoApp.

---

## ?? URL Base

```
https://localhost:5001/api/todos
```

---

## ?? Ejemplos con cURL

### 1. Obtener Todas las Tareas

**Request:**
```bash
curl -X GET https://localhost:5001/api/todos \
  -H "accept: application/json" \
  --insecure
```

**Response (200 OK):**
```json
[
  {
    "id": 1,
    "title": "Completar informe mensual",
    "isComplete": false
  },
  {
    "id": 2,
    "title": "Revisar código",
    "isComplete": true
  }
]
```

---

### 2. Obtener una Tarea por ID

**Request:**
```bash
curl -X GET https://localhost:5001/api/todos/1 \
  -H "accept: application/json" \
  --insecure
```

**Response (200 OK):**
```json
{
  "id": 1,
  "title": "Completar informe mensual",
  "isComplete": false
}
```

**Response (404 Not Found):**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404
}
```

---

### 3. Crear una Nueva Tarea

**Request:**
```bash
curl -X POST https://localhost:5001/api/todos \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d "{\"title\":\"Preparar presentación\",\"isComplete\":false}" \
  --insecure
```

**Response (201 Created):**
```json
{
  "id": 3,
  "title": "Preparar presentación",
  "isComplete": false
}
```

**Headers:**
```
Location: https://localhost:5001/api/todos/3
```

**Response (400 Bad Request) - Título vacío:**
```
"El título no puede estar vacío"
```

---

### 4. Actualizar una Tarea Existente

**Request:**
```bash
curl -X PUT https://localhost:5001/api/todos/1 \
  -H "accept: application/json" \
  -H "Content-Type: application/json" \
  -d "{\"title\":\"Informe completado\",\"isComplete\":true}" \
  --insecure
```

**Response (200 OK):**
```json
{
  "id": 1,
  "title": "Informe completado",
  "isComplete": true
}
```

**Response (404 Not Found) - ID no existe:**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404
}
```

---

### 5. Eliminar una Tarea

**Request:**
```bash
curl -X DELETE https://localhost:5001/api/todos/1 \
  -H "accept: */*" \
  --insecure
```

**Response (204 No Content):**
```
(Sin contenido en el cuerpo)
```

**Response (404 Not Found):**
```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
  "title": "Not Found",
  "status": 404
}
```

---

## ?? Ejemplos con PowerShell

### 1. Obtener Todas las Tareas

```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/todos" `
  -Method Get `
  -Headers @{"accept"="application/json"} `
  -SkipCertificateCheck
```

### 2. Obtener una Tarea por ID

```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/todos/1" `
  -Method Get `
  -Headers @{"accept"="application/json"} `
  -SkipCertificateCheck
```

### 3. Crear una Nueva Tarea

```powershell
$body = @{
    title = "Nueva tarea desde PowerShell"
    isComplete = $false
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/todos" `
  -Method Post `
  -Headers @{"accept"="application/json"; "Content-Type"="application/json"} `
  -Body $body `
  -SkipCertificateCheck
```

### 4. Actualizar una Tarea

```powershell
$body = @{
 title = "Tarea actualizada desde PowerShell"
    isComplete = $true
} | ConvertTo-Json

Invoke-RestMethod -Uri "https://localhost:5001/api/todos/1" `
  -Method Put `
  -Headers @{"accept"="application/json"; "Content-Type"="application/json"} `
  -Body $body `
  -SkipCertificateCheck
```

### 5. Eliminar una Tarea

```powershell
Invoke-RestMethod -Uri "https://localhost:5001/api/todos/1" `
  -Method Delete `
  -SkipCertificateCheck
```

---

## ?? Ejemplos con JavaScript/Fetch

### 1. Obtener Todas las Tareas

```javascript
fetch('https://localhost:5001/api/todos', {
  method: 'GET',
  headers: {
    'accept': 'application/json'
  }
})
.then(response => response.json())
.then(data => console.log(data))
.catch(error => console.error('Error:', error));
```

### 2. Crear una Nueva Tarea

```javascript
fetch('https://localhost:5001/api/todos', {
  method: 'POST',
  headers: {
    'accept': 'application/json',
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    title: 'Nueva tarea desde JavaScript',
    isComplete: false
  })
})
.then(response => response.json())
.then(data => console.log('Tarea creada:', data))
.catch(error => console.error('Error:', error));
```

### 3. Actualizar una Tarea

```javascript
fetch('https://localhost:5001/api/todos/1', {
  method: 'PUT',
  headers: {
    'accept': 'application/json',
    'Content-Type': 'application/json'
  },
  body: JSON.stringify({
    title: 'Tarea actualizada',
    isComplete: true
  })
})
.then(response => response.json())
.then(data => console.log('Tarea actualizada:', data))
.catch(error => console.error('Error:', error));
```

### 4. Eliminar una Tarea

```javascript
fetch('https://localhost:5001/api/todos/1', {
  method: 'DELETE',
  headers: {
    'accept': '*/*'
  }
})
.then(response => {
  if (response.ok) {
    console.log('Tarea eliminada correctamente');
  }
})
.catch(error => console.error('Error:', error));
```

---

## ?? Ejemplos con C# HttpClient

### 1. Obtener Todas las Tareas

```csharp
using System.Net.Http;
using System.Net.Http.Json;

var client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:5001");

var todos = await client.GetFromJsonAsync<List<TodoItem>>("/api/todos");
foreach (var todo in todos)
{
    Console.WriteLine($"{todo.Id}: {todo.Title} - {(todo.IsComplete ? "?" : "?")}");
}
```

### 2. Crear una Nueva Tarea

```csharp
var newTodo = new TodoItem 
{ 
    Title = "Nueva tarea desde C#",
    IsComplete = false
};

var response = await client.PostAsJsonAsync("/api/todos", newTodo);
var createdTodo = await response.Content.ReadFromJsonAsync<TodoItem>();

Console.WriteLine($"Tarea creada con ID: {createdTodo.Id}");
```

### 3. Actualizar una Tarea

```csharp
var updatedTodo = new TodoItem 
{ 
    Title = "Tarea actualizada",
  IsComplete = true
};

var response = await client.PutAsJsonAsync("/api/todos/1", updatedTodo);
var result = await response.Content.ReadFromJsonAsync<TodoItem>();

Console.WriteLine($"Tarea actualizada: {result.Title}");
```

### 4. Eliminar una Tarea

```csharp
var response = await client.DeleteAsync("/api/todos/1");

if (response.IsSuccessStatusCode)
{
    Console.WriteLine("Tarea eliminada correctamente");
}
```

---

## ?? Ejemplos con Python (requests)

### Instalación
```bash
pip install requests
```

### 1. Obtener Todas las Tareas

```python
import requests

response = requests.get('https://localhost:5001/api/todos', verify=False)
todos = response.json()

for todo in todos:
    status = "?" if todo['isComplete'] else "?"
    print(f"{todo['id']}: {todo['title']} - {status}")
```

### 2. Crear una Nueva Tarea

```python
import requests

new_todo = {
  'title': 'Nueva tarea desde Python',
    'isComplete': False
}

response = requests.post(
    'https://localhost:5001/api/todos',
    json=new_todo,
    verify=False
)

created_todo = response.json()
print(f"Tarea creada con ID: {created_todo['id']}")
```

### 3. Actualizar una Tarea

```python
import requests

updated_todo = {
    'title': 'Tarea actualizada desde Python',
 'isComplete': True
}

response = requests.put(
    'https://localhost:5001/api/todos/1',
    json=updated_todo,
    verify=False
)

result = response.json()
print(f"Tarea actualizada: {result['title']}")
```

### 4. Eliminar una Tarea

```python
import requests

response = requests.delete('https://localhost:5001/api/todos/1', verify=False)

if response.status_code == 204:
    print("Tarea eliminada correctamente")
```

---

## ?? Flujo Completo de Ejemplo

### Escenario: Gestión de Tareas Diarias

```bash
# 1. Ver todas las tareas actuales
curl -X GET https://localhost:5001/api/todos --insecure

# 2. Crear tarea: "Revisar correos"
curl -X POST https://localhost:5001/api/todos \
  -H "Content-Type: application/json" \
  -d "{\"title\":\"Revisar correos\",\"isComplete\":false}" \
  --insecure

# 3. Crear tarea: "Preparar reunión"
curl -X POST https://localhost:5001/api/todos \
  -H "Content-Type: application/json" \
  -d "{\"title\":\"Preparar reunión\",\"isComplete\":false}" \
  --insecure

# 4. Ver todas las tareas (deberían ser 2)
curl -X GET https://localhost:5001/api/todos --insecure

# 5. Marcar "Revisar correos" como completada (ID 1)
curl -X PUT https://localhost:5001/api/todos/1 \
  -H "Content-Type: application/json" \
  -d "{\"title\":\"Revisar correos\",\"isComplete\":true}" \
  --insecure

# 6. Verificar la tarea actualizada
curl -X GET https://localhost:5001/api/todos/1 --insecure

# 7. Eliminar la tarea completada
curl -X DELETE https://localhost:5001/api/todos/1 --insecure

# 8. Ver tareas restantes
curl -X GET https://localhost:5001/api/todos --insecure
```

---

## ?? Notas Importantes

### Certificado SSL

En desarrollo, usamos un certificado autofirmado. Por eso:
- **cURL**: Usar `--insecure` o `-k`
- **PowerShell**: Usar `-SkipCertificateCheck`
- **Python**: Usar `verify=False`

### Formato JSON

Asegúrate de que el JSON esté bien formado:
```json
{
  "title": "Mi tarea",
"isComplete": false
}
```

### Headers Requeridos

Para POST y PUT:
```
Content-Type: application/json
```

---

## ?? Casos de Prueba

### ? Casos Exitosos

1. **Crear tarea válida**: título no vacío
2. **Obtener tarea existente**: ID válido
3. **Actualizar tarea existente**: ID válido + datos válidos
4. **Eliminar tarea existente**: ID válido

### ? Casos de Error

1. **Crear con título vacío**: retorna 400
2. **Obtener tarea inexistente**: retorna 404
3. **Actualizar tarea inexistente**: retorna 404
4. **Eliminar tarea inexistente**: retorna 404

---

## ?? Acceso a Swagger UI

La forma más fácil de probar la API es usar Swagger:

1. Ejecutar la aplicación:
   ```bash
   cd src/TodoApp.Api
   dotnet run
   ```

2. Abrir navegador en:
   ```
   https://localhost:5001/swagger
   ```

3. Usar la interfaz interactiva para probar todos los endpoints

---

## ?? Referencias

- [Documentación de la API](README.md)
- [PRD Completo](PRD-TodoApp.md)
- [Comandos Útiles](COMANDOS.md)

---

**Tip**: Para entornos de producción, siempre usa HTTPS válido y maneja los errores apropiadamente.
