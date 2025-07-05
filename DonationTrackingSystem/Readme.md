# 🎯 Donation Management System – Kurumsal Proje Yapısı

Bu doküman, **Bağış Takip ve Muhasebe Yönetimi Sistemi** için hem backend (ASP.NET Core Clean Architecture) hem de frontend (React + Vite) projelerini tek bir ana klasörde, kurumsal ve sürdürülebilir bir yapıda nasıl organize edeceğinizi detaylıca açıklar.

---

## 📁 Ana Klasör Yapısı

```
DonationManagementSystem/
│
├── backend/                      ← ASP.NET Core Web API (Clean Architecture)
│   ├── API/                      ← Sunum Katmanı (UI / Controller)
│   │   ├── Controllers/
│   │   ├── Middleware/
│   │   ├── Filters/
│   │   ├── Extensions/
│   │   ├── Program.cs
│   │   └── appsettings.json
│   │
│   ├── Application/             ← İş Mantığı Katmanı
│   │   ├── DTOs/
│   │   ├── Interfaces/
│   │   ├── Validators/
│   │   ├── Features/            ← CQRS: Commands & Queries
│   │   │   ├── Bagislar/
│   │   │   ├── Kullanici/
│   │   │   ├── Muhasebe/
│   │   ├── Mappings/            ← AutoMapper profilleri
│   │   └── Behaviors/           ← Pipeline davranışları (Validation, Logging)
│   │
│   ├── Domain/                  ← Saf Domain Katmanı
│   │   ├── Entities/
│   │   ├── Enums/
│   │   ├── ValueObjects/
│   │   └── Common/              ← BaseEntity, AuditableEntity
│   │
│   ├── Infrastructure/          ← Yardımcı servisler
│   │   ├── PDF/                 ← QuestPDF ile PDF işlemleri
│   │   ├── Excel/               ← ClosedXML ile Excel işlemleri
│   │   ├── Email/
│   │   ├── Logging/
│   │   └── FileSystem/
│   │
│   ├── Persistence/             ← Veri Katmanı
│   │   ├── Context/             ← AppDbContext.cs
│   │   ├── Repositories/        ← Generic & özel repository
│   │   ├── Configurations/      ← Fluent API mapping
│   │   └── Migrations/
│   │
│   ├── Shared/                  ← Sabitler, yardımcılar
│   └── DonationTrackingSystem.sln
│
├── frontend/                    ← React + Vite Arayüz
│   ├── public/
│   │   └── favicon.svg
│   │
│   ├── src/
│   │   ├── api/                 ← Axios ve auth interceptor
│   │   │   ├── axios.js
│   │   │   └── authInterceptor.js
│   │
│   │   ├── auth/                ← Giriş / Kayıt işlemleri
│   │   │   ├── LoginPage.jsx
│   │   │   ├── RegisterPage.jsx
│   │   │   └── useAuth.js
│   │
│   │   ├── components/          ← Ortak UI bileşenleri
│   │   │   ├── Navbar.jsx
│   │   │   ├── Sidebar.jsx
│   │   │   └── Footer.jsx
│   │
│   │   ├── pages/               ← Sayfa bazlı ekranlar
│   │   │   ├── Dashboard/
│   │   │   ├── Bagislar/
│   │   │   ├── Muhasebe/
│   │   │   ├── Raporlama/
│   │   │   ├── Export/
│   │   │   └── Ayarlar/
│   │
│   │   ├── routes/              ← Sayfa yönlendirme
│   │   │   ├── AppRouter.jsx
│   │   │   └── ProtectedRoute.jsx
│   │
│   │   ├── services/            ← API çağrıları
│   │   │   ├── authService.js
│   │   │   ├── bagisService.js
│   │   │   └── exportService.js
│   │
│   │   ├── styles/              ← Tailwind config / özel stiller
│   │   │   └── tailwind.config.js
│   │
│   │   ├── App.jsx
│   │   └── main.jsx
│   │
│   ├── .env                    ← API_BASE_URL gibi değişkenler
│   ├── index.html
│   ├── package.json
│   └── vite.config.js
│
├── docker-compose.yml         ← (Opsiyonel) API + DB container tanımı
├── README.md
```

---

## 📖 Açıklama ve Katmanların Görevleri

### `backend/`
- **API/**: Kullanıcıdan gelen HTTP isteklerini karşılayan controller ve middleware katmanı.
- **Application/**: Tüm iş mantığı, CQRS komutları, validasyonlar ve servis arayüzleri burada.
- **Domain/**: Saf domain modelleri, entity ve enum tanımları, iş kuralları.
- **Infrastructure/**: Dış bağımlılıklar (PDF, Excel, e-posta, dosya sistemi, logging).
- **Persistence/**: EF Core context, repository ve migration işlemleri.
- **Shared/**: Ortak sabitler, yardımcı fonksiyonlar.
- **DonationTrackingSystem.sln**: .NET çözüm dosyası.

### `frontend/`
- **public/**: Statik dosyalar.
- **src/api/**: Axios instance ve auth interceptor.
- **src/auth/**: Giriş/kayıt işlemleri ve authentication hook’ları.
- **src/components/**: Navbar, Sidebar gibi ortak UI bileşenleri.
- **src/pages/**: Dashboard, bağışlar, muhasebe, raporlama, dışa aktarım ve ayarlar sayfaları.
- **src/routes/**: React Router ile sayfa yönlendirme ve korumalı rotalar.
- **src/services/**: API ile haberleşen servisler.
- **src/styles/**: Tailwind ve özel stiller.
- **App.jsx, main.jsx**: Uygulama giriş noktaları.
- **.env**: Ortam değişkenleri (örn. API adresi).

### Kök Düzey
- **docker-compose.yml**: API ve veritabanı için container tanımı (opsiyonel).
- **README.md**: Proje dokümantasyonu.

---

## ✅ Neden Bu Yapı?

- **Kurumsal Standart**: Backend ve frontend tamamen ayrık, bağımsız geliştirilebilir ve deploy edilebilir.
- **Modülerlik**: Her katman kendi sorumluluğunda, test edilebilir ve sürdürülebilir.
- **Genişletilebilirlik**: Yeni modüller veya servisler kolayca eklenebilir.
- **Takım Çalışmasına Uygun**: Frontend ve backend ekipleri paralel çalışabilir.
- **CI/CD ve Docker**: Modern DevOps süreçlerine hazır.

---

## 🚀 Kurulum ve Geliştirme

### Backend için:
1. `cd backend`
2. Gerekli .NET projelerini oluşturun ve referansları ekleyin.
3. `dotnet ef database update` ile veritabanını oluşturun.
4. `dotnet run` ile API’yi başlatın.

### Frontend için:
1. `cd frontend`
2. `npm install`
3. `.env` dosyasına API adresini yazın.
4. `npm run dev` ile React arayüzünü başlatın.

### Docker ile:
- `docker-compose up` komutu ile hem API hem de veritabanı ayağa kaldırılabilir.