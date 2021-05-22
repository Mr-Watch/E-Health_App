import requests

url = "https://www.vrisko.gr/efimeries-farmakeion/sykies/"

headers = {
  'User-Agent': 'Mozilla/5.0 (Windows NT 10.0; Win64; x64)',
}

response = requests.request("GET", url, headers=headers)

print(response.text)