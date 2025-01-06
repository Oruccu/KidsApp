# 📚 **Mini Keşif APP**
### **Etkileşimli Çocuk Eğitim Uygulaması**

---

## 🚀 **Proje Özeti**
**Mini Keşif APP**, çocukların eğlenerek öğrenmesini sağlayan interaktif bir mobil uygulamadır. Renk eşleştirme, sayı sayma gibi eğitici mini oyunlar ve sesli kitaplar içermektedir. Ayrıca, ebeveynler çocuklarının ilerlemelerini bir ebeveyn paneli üzerinden takip edebilir.

**🛠️ Teknolojiler:**
- **Back-End:** ASP.NET Core Web API
- **Front-End:** React Native (Expo)
- **Veritabanı:** Entity Framework Core (SQL Server)

### 🎨 **Figma Tasarımı:**  
[**<span style="color:blue;">Interactive Kids App Tasarımı</span>**](https://www.figma.com/design/SMt8G71X1Dm0QSYUWQnHw4/InteractiveKidsApp?node-id=13-50&t=pb76WNTbYMnikEwH-1)



## 📂 **Proje Yapısı**

### **1. Back-End (ASP.NET Core Web API)**
**Klasör Yapısı:**
```
/InteractiveKidsAppBackend
├── API (Web API projesi)
│   ├── Controllers
│   ├── Middleware
│   ├── Program.cs
│   ├── appsettings.json
│
├── Business (İş Mantığı)
│   ├── Services
│   ├── Interfaces
│
├── DataAccess (Veritabanı Erişimi)
│   ├── Context
│   ├── Repositories
│
├── Entities (Veritabanı Modelleri)
│   ├── User.cs
│   ├── GameResult.cs
│   ├── StoryProgress.cs
│   ├── AudioBook.cs
```

**Temel Özellikler:**
- JWT tabanlı kimlik doğrulama.
- API endpoint'leri:
   - `/api/auth/register` → Kullanıcı kaydı.
   - `/api/auth/login` → JWT ile giriş.
   - `/api/games/saveResult` → Oyun sonucu kaydı.
   - `/api/games/getResults` → Oyun sonuçlarını listeleme.
   - `/api/audiobooks/list` → Sesli kitapları listeleme.
   - `/api/audiobooks/get/{id}` → Kitap detayları.
   - `/api/parent/getProgress/{childId}` → İlerleme bilgileri.

**Veritabanı Modelleri:**
- **User:** Çocuk ve ebeveyn kullanıcılar.
- **GameResult:** Oyun sonuçları.
- **StoryProgress:** Hikaye ilerlemeleri.
- **AudioBook:** Sesli kitap bilgileri.

### **2. Front-End (React Native + Expo)**

**Klasör Yapısı:**
```
/InteractiveKidsAppFrontend
├── App.js
├── assets
├── components
│   ├── CustomButton.js
│   ├── ScoreCard.js
├── screens
│   ├── SplashScreen.js
│   ├── HomeScreen.js
│   ├── GamesScreen.js
│   ├── AudioBooksScreen.js
│   ├── StoriesScreen.js
│   ├── ParentDashboardScreen.js
├── navigation
│   ├── TabNavigator.js
│   ├── AuthNavigator.js
├── styles
│   ├── colors.js
│   ├── globalStyles.js
```

**🧒 Çocuk Modu:**
- **Ana Sayfa:** Mini oyunlar, sesli kitaplar ve hikayeler.
- **Mini Oyunlar:**
   - Renk Eşleştirme
   - Sayı Sayma
   - Hayvan Sesleri
- **Sesli Kitaplar:** Kitap listesi ve oynatıcı.
- **Hikayeler:** İlerlemeye dayalı hikaye okuma.

**👪 Ebeveyn Modu:**
- **Giriş Sayfası:** JWT kimlik doğrulama.
- **İlerleme Takibi:** Çocukların ilerleme durumu grafiklerle gösterilir.
- **Raporlama:** Haftalık ve aylık raporlar.

**🎨 UI Renk Teması:**
- **Erkek Modu:** `#93C82C`
- **Kız Modu:** `#F84794`

**🧭 Navigasyon:**
- **Tab Navigation:** Çocuk ve ebeveyn modları arasında geçiş.
- **Stack Navigation:** Sayfalar arasında geçiş.

**🔌 Entegrasyonlar:**
- **Axios:** API istekleri.
- **JWT Authentication:** Yetkilendirme.
- **expo-av:** Sesli kitap oynatma.
- **AsyncStorage:** Oturum verilerini saklama.

---

## 🔑 **Kurulum Adımları**

### **Back-End (ASP.NET Core Web API):**
```bash
cd InteractiveKidsAppBackend
cd API
# Bağımlılıkları yükle
 dotnet restore
# Projeyi başlat
 dotnet run
```

### **Front-End (React Native + Expo):**
```bash
cd InteractiveKidsAppFrontend
# Bağımlılıkları yükle
npm install
# Uygulamayı başlat
npx expo start
```

---

## 🛡️ **Güvenlik Önlemleri:**
- JWT ile güvenli kimlik doğrulama.
- Hassas bilgilerin çevresel değişkenlerle korunması.
- Veri şifreleme.

---

## 🤝 **Katkıda Bulunma**
Katkıda bulunmak için lütfen bir **Pull Request** oluşturun veya bize ulaşın.

---

## 📄 **Lisans**
Bu proje **MIT Lisansı** altında yayınlanmıştır.

---

🎯 **Hazır mısınız? Öyleyse keşfetmeye başlayın! 🚀**

