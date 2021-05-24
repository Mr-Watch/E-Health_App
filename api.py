from biomedlexicon import search_biomedlexicon
from scrapers.doctoranytime import get_doctors_list
from healthatlas import get_hospitals_based_on_geoId, get_pharmacies_based_on_geoId, get_geoIds
import symptom_api
from farmakeia import get_pharmacies, get_url
from flask import Flask
from flask import jsonify
from flask import request

app = Flask(__name__)
api_version = '/ath/api/v0.1/'


@app.route(api_version+'biomedlexicon/<string:search_term>', methods=['GET'])
def get_biomedlexicon_translation(search_term):
    return jsonify(search_biomedlexicon(search_term))


@app.route(api_version+'farmakeia.gr/<string:pharmacy_type>/<string:district>/<string:sub_district>', methods=['GET'])
def get_farmakeia_gr_pharmacies(pharmacy_type, district, sub_district):
    return jsonify(get_pharmacies(get_url(pharmacy_type, district, sub_district)))


@app.route(api_version+'doctoranytime/', methods=['GET'])
def get_doctors():
    return jsonify(get_doctors_list('https://www.doctoranytime.gr/s/Aimatologos/attiki'))


# Healthatlas api entry points

@app.route(api_version+'healthatlas/hospitals/<string:geo_id>', methods=['GET'])
def get_hospitals_based_on_geoId_flask(geo_id: str):
    return jsonify(get_hospitals_based_on_geoId(geo_id))


@app.route(api_version+'healthatlas/pharmacies/<string:geo_id>', methods=['GET'])
def get_pharmacies_based_on_geoId_flask(geo_id: str):
    return jsonify(get_pharmacies_based_on_geoId(geo_id))


@app.route(api_version+'healthatlas/', methods=['GET'])
def get_geoIds_flask():
    return jsonify(get_geoIds())


# Webmd api entry points

@app.route(api_version+'webmd/symptoms/id/<string:body_section>/', methods=['GET'])
def get_body_part_id_flask(body_section: str):
    return jsonify(symptom_api.get_body_part_id(body_section))


@app.route(api_version+'webmd/symptoms/id/<string:body_section>/<string:section_part>', methods=['GET'])
def get_body_part_ids_flask(body_section: str, section_part: str):
    id1, id2 = symptom_api.get_body_part_ids(body_section, section_part)
    return jsonify(id1+id2)


@app.route(api_version+'webmd/symptoms/<string:body_section>/', methods=['GET'])
def make_body_symptoms_single_api_request_flask(body_section: str):
    return jsonify(symptom_api.make_body_symptoms_single_api_request(body_section))


@app.route(api_version+'webmd/symptoms/<string:body_section>/<string:section_part>', methods=['GET'])
def make_body_symptoms_api_request_flask(body_section: str, section_part: str):
    return jsonify(symptom_api.make_body_symptoms_api_request(body_section, section_part))


@ app.route(api_version+'webmd/conditions/<string:age>/<string:gender>', methods=['POST'])
def make_symptoms_api_request_flask(age: str, gender: str):
    request_arguments = request.data.decode('utf-8')
    return jsonify(symptom_api.make_symptoms_api_request(age, gender, request_arguments))


@app.route(api_version+'webmd/body-part-ids/', methods=['GET'])
def body_part_ids_flask():
    return jsonify(symptom_api.get_body_part_ids())

# General entry points


@ app.route('/', methods=['GET'])
def default_route():
    return jsonify({'api_version': '0.1'})


if __name__ == "__main__":
    app.run(debug=True)
