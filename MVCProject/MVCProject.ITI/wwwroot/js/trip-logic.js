const TOTAL = 352;
const trips = [
    { from: 'Cairo', to: 'Alexandria', date: '2024-01-15', km: 220, traffic: 'Medium', cost: 352 },
    { from: 'Cairo', to: 'Giza Pyramids', date: '2024-01-12', km: 25, traffic: 'Low', cost: 30 },
    { from: 'Cairo', to: 'Sharm El Sheikh', date: '2024-01-08', km: 480, traffic: 'Heavy', cost: 702 },
    { from: 'Cairo', to: 'Hurghada', date: '2024-01-05', km: 460, traffic: 'Low', cost: 653 },
    { from: 'Cairo', to: 'Luxor', date: '2023-12-28', km: 670, traffic: 'Medium', cost: 940 },
];
let passengers = [];

function showTrip() {
    document.getElementById('page-trip').classList.add('active');
    document.getElementById('page-history').classList.remove('active');
}
function showHistory() {
    document.getElementById('page-history').classList.add('active');
    document.getElementById('page-trip').classList.remove('active');
    renderHistory();
}
function setNav(el) { document.querySelectorAll('.nav-btn').forEach(b => b.classList.remove('active')); el.classList.add('active'); }

function pillCls(t) { return t === 'Low' ? 'pill-low' : t === 'Medium' ? 'pill-medium' : 'pill-heavy'; }
function renderHistory() {
    document.getElementById('history-list').innerHTML = trips.map((t, i) => `
    <div class="h-item" style="animation-delay:${i * .06}s" onclick="showTrip();setNav(document.querySelectorAll('.nav-btn')[0])">
      <div class="h-av"><svg viewBox="0 0 24 24" fill="none" stroke-width="2" stroke-linecap="round"><circle cx="12" cy="10" r="3"/><path d="M12 2a8 8 0 018 8c0 6-8 14-8 14S4 16 4 10a8 8 0 018-8z"/></svg></div>
      <div class="h-info">
        <div class="h-route">${t.from}, Egypt → ${t.to}, Egypt</div>
        <div class="h-meta">
          <svg width="11" height="11" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round"><rect x="3" y="4" width="18" height="18" rx="2"/><line x1="16" y1="2" x2="16" y2="6"/><line x1="8" y1="2" x2="8" y2="6"/><line x1="3" y1="10" x2="21" y2="10"/></svg>
          ${t.date} &nbsp;${t.km} km &nbsp;<span class="t-pill ${pillCls(t.traffic)}">${t.traffic}</span>
        </div>
      </div>
      <div style="text-align:right;margin-left:14px;"><div class="h-cost">${t.cost}</div><div class="h-unit">EGP</div></div>
      <div class="h-acts">
        <button class="del-btn" onclick="delTrip(event,${i})"><svg viewBox="0 0 24 24" fill="none" stroke-width="2" stroke-linecap="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/><path d="M10 11v6M14 11v6"/><path d="M9 6V4h6v2"/></svg></button>
        <button class="arr-btn"><svg viewBox="0 0 24 24" fill="none" stroke-width="2.5" stroke-linecap="round"><polyline points="9 18 15 12 9 6"/></svg></button>
      </div>
    </div>`).join('');
}
function delTrip(e, i) { e.stopPropagation(); trips.splice(i, 1); renderHistory(); }

function toggleSec(hdr) {
    const body = hdr.nextElementSibling, chev = hdr.querySelector('.chev');
    const open = chev.classList.contains('open');
    if (open) { body.style.maxHeight = '0'; chev.classList.remove('open'); }
    else { body.style.maxHeight = body.scrollHeight + 'px'; chev.classList.add('open'); }
}

function addPassenger() {
    const inp = document.getElementById('pass-name-input');
    const name = inp.value.trim();
    if (!name) return;
    passengers.push(name);
    inp.value = '';
    renderPassengers();
    // Expand section body to fit new content
    const body = document.getElementById('sp-empty').closest('.sb');
    if (body) setTimeout(() => { body.style.maxHeight = body.scrollHeight + 'px'; }, 20);
}
function removePassenger(i) {
    passengers.splice(i, 1);
    renderPassengers();
    const body = document.getElementById('sp-empty').closest('.sb');
    if (body) setTimeout(() => { body.style.maxHeight = body.scrollHeight + 'px'; }, 20);
}
function renderPassengers() {
    const empty = document.getElementById('sp-empty');
    const list = document.getElementById('pass-list');
    const sum = document.getElementById('sp-summary');
    const amt = document.getElementById('sp-amount');
    if (passengers.length === 0) {
        empty.style.display = 'flex'; list.innerHTML = ''; sum.style.display = 'none';
    } else {
        empty.style.display = 'none';
        const each = Math.round(TOTAL / (passengers.length + 1));
        list.innerHTML = passengers.map((n, i) => `
      <div class="pass-item">
        <div class="pi-left">
          <div class="p-av"><svg viewBox="0 0 24 24" fill="none" stroke-width="2.2" stroke-linecap="round"><path d="M20 21v-2a4 4 0 00-4-4H8a4 4 0 00-4 4v2"/><circle cx="12" cy="7" r="4"/></svg></div>
          <div><div class="p-name">${n}</div><div class="p-share">EGP ${each}</div></div>
        </div>
        <button class="p-del" onclick="removePassenger(${i})"><svg viewBox="0 0 24 24" fill="none" stroke-width="2" stroke-linecap="round"><polyline points="3 6 5 6 21 6"/><path d="M19 6l-1 14H6L5 6"/><path d="M10 11v6M14 11v6"/><path d="M9 6V4h6v2"/></svg></button>
      </div>`).join('');
        amt.textContent = 'EGP ' + each;
        sum.style.display = 'block';
    }
}

let modalMap = null;
function openMapModal() {
    document.getElementById('map-modal').classList.add('open');
    setTimeout(() => {
        if (!modalMap) {
            modalMap = L.map('modal-map').setView([30.5, 30.5], 7);
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', { attribution: '© OpenStreetMap' }).addTo(modalMap);
            const ico = L.divIcon({
                className: '',
                html: `<div style="width:28px;height:28px;background:#2DD4BF;border-radius:50%;border:3px solid #fff;box-shadow:0 2px 8px rgba(0,0,0,.25);display:flex;align-items:center;justify-content:center;"><svg width="12" height="12" viewBox="0 0 24 24" fill="none" stroke="white" stroke-width="3" stroke-linecap="round"><circle cx="12" cy="10" r="3"/><path d="M12 2a8 8 0 018 8c0 6-8 14-8 14S4 16 4 10a8 8 0 018-8z"/></svg></div>`,
                iconSize: [28, 28], iconAnchor: [14, 28]
            });
            const c = L.latLng(30.0444, 31.2357), a = L.latLng(31.2001, 29.9187);
            L.marker(c, { icon: ico }).addTo(modalMap).bindPopup('<b>Cairo</b>');
            L.marker(a, { icon: ico }).addTo(modalMap).bindPopup('<b>Alexandria</b>');
            L.polyline([c, a], { color: '#2DD4BF', weight: 4, dashArray: '10,7', opacity: .9 }).addTo(modalMap);
            modalMap.fitBounds([c, a], { padding: [48, 48] });
        } else { modalMap.invalidateSize(); }
    }, 220);
}
function closeMap() { document.getElementById('map-modal').classList.remove('open'); }
function handleOvClick(e) { if (e.target === document.getElementById('map-modal')) closeMap(); }