from bs4 import BeautifulSoup
import requests
import json


def create_efimereyonta_farmakeia_lookup():
    create_lookup("https://farmakeia.gr/efimereyonta-farmakeia-perioxes")


def create_farmakeia_lookup():
    create_lookup("https://farmakeia.gr/farmakeia-perioxes")


def create_lookup(url):
    farmakeia_response = requests.get(url)
    farmakeia_response_soup = BeautifulSoup(farmakeia_response.content, "lxml")

    farmakeia_cards = farmakeia_response_soup.select_one(".col-12").select(".card")

    link_dict = {}
    for card in farmakeia_cards:
        farmakeia_links = card.find_all("a", href=True)
        inner_link_dict = {}
        for link in farmakeia_links:
            inner_link_dict[link.get_text()] = link["href"]
        text_description = card.select_one(".m-0").text[23:]
        link_dict[text_description] = inner_link_dict

    if url == "https://farmakeia.gr/farmakeia-perioxes":
        url = "farmakeia"
    else:
        url = "efimereyonta_farmakeia"

    with open("./" + url + ".json", "w") as file:
        json.dump(link_dict, file)
        file.close()


def get_url(pharmacy_type, district, sub_district):
    file_location = ""
    url = "https://farmakeia.gr"
    if pharmacy_type == "εφημερεύοντα":
        file_location = "./efimereyonta_farmakeia.json"
    elif pharmacy_type == "μη-εφημερεύοντα":
        file_location = "./farmakeia.json"
    else:
        return {"error": "improper argument type"}

    with open(file_location) as file:
        data = json.load(file)
    url += data[district][sub_district]
    return url


def get_pharmacies(url):
    farmakeia_dict = {}
    farmakeia_response = requests.get(url)
    farmakeia_response_soup = BeautifulSoup(farmakeia_response.content, "lxml")

    collection = farmakeia_response_soup.find_all("section")
    step = 0
    for card in collection:
        card_dict = {}
        farmakeio_name = card.find("div", class_="card-header")
        farmakeio_tel = card.find("div", class_="card-body").find_all("a")[1]
        farmakeio_dir = card.find("div", class_="card-body").find_all("a")[0]
        farmakeio_dir_ = (
            farmakeio_dir["href"].replace("\n", "").split("&destination=")[1].split(",")
        )
        farmakeio_name_ = farmakeio_name.get_text().replace("\n", "")
        farmakeio_tel_ = farmakeio_tel.get_text().replace("\n", "").strip()
        farmakeio_tel.decompose()
        farmakeio_name.decompose()
        farmakeio_dir.decompose()
        farmakeio_addr = (
            card.find("div", class_="card-body").get_text().replace("\n", "").strip()
        )

        card_dict = {
            "pharmacy_name": farmakeio_name_,
            "pharmacy_address": farmakeio_addr,
            "pharmacy_number": farmakeio_tel_,
            "pharmacy_map_cordinates": farmakeio_dir_,
        }
        farmakeia_dict[step] = card_dict
        step += 1
    return farmakeia_dict
    """
create_farmakeia_lookup()
create_efimereyonta_farmakeia_lookup()
print(get_url("μη-εφημερεύοντα","Αργολίδας","Ερμιόνη"))
"""
